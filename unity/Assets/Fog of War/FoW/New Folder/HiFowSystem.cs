//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Diagnostics;
using System.Threading;


using Debug = UnityEngine.Debug;

public class HiFowSystem : MonoBehaviour
{
    public static HiFowSystem Instance;
    public int worldSize = 512;
    public int textureSize = 128;


    static BetterList<Revealer> revealerList = new BetterList<Revealer>();
    static BetterList<Revealer> addedList = new BetterList<Revealer>();
    static BetterList<Revealer> removedList = new BetterList<Revealer>();



    private Color32[] color32s0;
    private Color32[] color32s1;


    private Vector3 originPos;





    public float delay = 0.5f;

    public float blendFactor { get; private set; }


    State state = State.Blending;
    private enum State
    {
        Blending,
        NeedUpdate,
        UpdateTexture0,
        UpdateTexture1,
    }

    void Awake()
    {
        Instance = this;
    }


    private float nextUpdate;
    public float updateFrequency = 0.1f;
    // Use this for initialization
    void Start()
    {
        #region 设置原点
        originPos = transform.position;
        originPos.x -= worldSize * 0.5f;
        originPos.z -= worldSize * 0.5f;
        #endregion

        var temp = textureSize * textureSize;
        color32s0 = new Color32[temp];
        color32s1 = new Color32[temp];

        UpdateBuffer();
        UpdateTexture();
        nextUpdate = Time.time + updateFrequency;

        thread = new Thread(ThreadUpdate);
        thread.Start();


    }

    private Thread thread;
    private float elapsed = 0;
    void ThreadUpdate()
    {
        Stopwatch sw = new Stopwatch();
        for (;;)
        {
            if (state == State.NeedUpdate)
            {
                sw.Reset();
                sw.Start();
                UpdateBuffer();
                sw.Stop();
                elapsed = 0.001f * (float)sw.ElapsedMilliseconds;
                state = State.UpdateTexture0;
            }
            Thread.Sleep(1);
        }
    }

    void OnDestroy()
    {
        if (thread != null)
        {
            Debug.Log(thread);
            thread.Abort();
            Debug.Log(thread);
            while (thread.IsAlive)
            {
                Thread.Sleep(1);
                thread = null;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(worldSize, 0, worldSize));
    }

    // Update is called once per frame
    void Update()
    {

        if (delay > 0f)
        {
            blendFactor = Mathf.Clamp01(blendFactor + Time.deltaTime / delay);
        }
        else blendFactor = 1f;

        if (state == State.Blending)
        {
            if (nextUpdate < Time.time)
            {
                nextUpdate = Time.time + updateFrequency;
                state = State.NeedUpdate;
            }
        }
        else if (state != State.NeedUpdate)
        {
            UpdateTexture();
        }
    }

    void UpdateBuffer()
    {
        if (addedList.size > 0)
        {
            lock (addedList)
            {
                while (addedList.size > 0)
                {
                    var tempIndex = addedList.size - 1;
                    revealerList.Add(addedList.buffer[tempIndex]);
                    addedList.RemoveAt(tempIndex);
                }
            }
        }
        if (removedList.size > 0)
        {
            lock (removedList)
            {
                while (removedList.size > 0)
                {
                    var tempIndex = removedList.size - 1;
                    removedList.Remove(removedList.buffer[tempIndex]);
                    removedList.RemoveAt(tempIndex);
                }
            }
        }
        float factor = (delay > 0f) ? Mathf.Clamp01(blendFactor + elapsed / delay) : 1f;
        for (int i = 0, imax = color32s0.Length; i < imax; ++i)
        {
            color32s0[i] = Color32.Lerp(color32s0[i], color32s1[i], factor);
            color32s1[i].r = 0;
        }
        float tempWorldToTex = (float)textureSize / worldSize;
        for (int i = 0; i < revealerList.size; i++)
        {
            Revealer temp = revealerList[i];
            if (!temp.isActive)
                continue;
            RevealUsingLOS(temp, tempWorldToTex);
        }
        RevealMap();
    }

    public Texture2D texture2D0;
    public Texture2D texture2D1;
    void UpdateTexture()
    {

        if (texture2D0 == null)
        {
            texture2D0 = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            texture2D1 = new Texture2D(textureSize, textureSize, TextureFormat.ARGB32, false);
            texture2D0.wrapMode = TextureWrapMode.Clamp;
            texture2D1.wrapMode = TextureWrapMode.Clamp;

            texture2D0.SetPixels32(color32s0);
            texture2D0.Apply();
            texture2D1.SetPixels32(color32s1);
            texture2D1.Apply();
            state = State.Blending;
        }
        else if (state == State.UpdateTexture0)
        {
            texture2D0.SetPixels32(color32s0);
            texture2D0.Apply();
            state = State.UpdateTexture1;
            blendFactor = 0f;
        }
        else if (state == State.UpdateTexture1)
        {
            texture2D1.SetPixels32(color32s1);
            texture2D1.Apply();
            state = State.Blending;
        }
    }

    void RevealUsingLOS(Revealer paramRevealer, float paramWorldToTex)
    {
        Vector3 tempPos = paramRevealer.pos - originPos;
        int xmin = Mathf.RoundToInt((tempPos.x - paramRevealer.range) * paramWorldToTex);
        int ymin = Mathf.RoundToInt((tempPos.z - paramRevealer.range) * paramWorldToTex);
        int xmax = Mathf.RoundToInt((tempPos.x + paramRevealer.range) * paramWorldToTex);
        int ymax = Mathf.RoundToInt((tempPos.z + paramRevealer.range) * paramWorldToTex);
        int cx = Mathf.RoundToInt(tempPos.x * paramWorldToTex);
        int cy = Mathf.RoundToInt(tempPos.z * paramWorldToTex);

        cx = Mathf.Clamp(cx, 0, textureSize - 1);//because of arry start from 0
        cy = Mathf.Clamp(cy, 0, textureSize - 1);

        Color32 white = Color.white;

        int range = Mathf.RoundToInt(paramRevealer.range * paramWorldToTex * paramRevealer.range * paramWorldToTex);
        for (int y = ymin; y < ymax; y++)
        {
            if (y > -1 && y < textureSize)
            {
                for (int x = xmin; x < xmax; x++)
                {
                    if (x > -1 && x < textureSize)
                    {
                        int index = y * textureSize + x;
                        int xd = x - cx;
                        int yd = y - cy;
                        int dist = xd * xd + yd * yd;
                        
                        if (dist < range)
                            color32s1[index] = white;
                    }
                }
            }
        }
    }

    void RevealMap()
    {
        for (int y = 0; y < textureSize; y++)
        {
            for (int x = 0; x < textureSize; x++)
            {
                int index = y * textureSize + x;
                Color32 tempC = color32s1[index];
                if (tempC.g < tempC.r)
                {
                    tempC.g = tempC.r;
                    color32s1[index] = tempC;
                }
            }
        }
    }

    public static Revealer CreateRevealer()
    {
        Revealer temp = new Revealer();
        temp.isActive = false;
        lock (addedList)
        {
            addedList.Add(temp);
        }
        return temp;
    }

    public static void DeleteRevealer(Revealer param)
    {
        lock (removedList)
        {
            removedList.Add(param);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="param"></param>
    /// <returns>position</returns>
    public bool IsVisible(Vector3 param)
    {
        if (color32s0 == null)
            return false;
        param -= originPos;
        float tempWorldToTex = (float)textureSize / worldSize;
        int tempCx = Mathf.RoundToInt(param.x * tempWorldToTex);
        int tempCy = Mathf.RoundToInt(param.z * tempWorldToTex);
        tempCx = Mathf.Clamp(tempCx, 0, textureSize - 1);
        tempCy = Mathf.Clamp(tempCy, 0, textureSize - 1);
        int tempIndex = tempCx + tempCy * textureSize;
        return color32s0[tempIndex].r > 0 || color32s1[tempIndex].r > 0;
    }

    public class Revealer
    {
        public bool isActive;
        public Vector3 pos;
        public float range;
    }
}
