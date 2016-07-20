//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class HiFowSystem : MonoBehaviour
{

    public static HiFowSystem Instance;



    static BetterList<Revealer> addedList = new BetterList<Revealer>();
    static BetterList<Revealer> removedList = new BetterList<Revealer>();



    private Color32[] buffer0Color32;
    private Color32[] buffer1Color32;
    public int worldSize = 512;
    public int textureSize = 128;

    private Vector3 originPos;

    // Use this for initialization
    void Start()
    {
        #region 设置原点
        originPos = transform.position;
        originPos.x -= worldSize * 0.5f;
        originPos.z -= worldSize * 0.5f;
        #endregion
    }

    // Update is called once per frame
    void Update()
    {

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
        if (buffer0Color32 == null)
            return false;
        param -= originPos;
        float tempWorldToTex = (float)textureSize / worldSize;
        int tempCx = Mathf.RoundToInt(param.x * tempWorldToTex);
        int tempCz = Mathf.RoundToInt(param.z * tempWorldToTex);
        tempCx = Mathf.Clamp(tempCx, 0, textureSize - 1);
        tempCz = Mathf.Clamp(tempCz, 0, textureSize - 1);
        int tempIndex = tempCx + tempCz * textureSize;
        return buffer0Color32[tempIndex].r > 0 || buffer1Color32[tempIndex].r > 0;
    }

    public class Revealer
    {
        public bool isActive;
        public Vector3 pos;
        public float range;
        public bool[] cachedBuffer;
        public int cachedSize;
        public int cachedX;
        public int cachedY;
    }
}
