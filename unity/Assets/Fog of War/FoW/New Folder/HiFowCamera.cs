//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class HiFowCamera : MonoBehaviour
{
    public Color unexplored = Color.gray;
    public Color explored = Color.gray;

    private Shader shader;
    private HiFowSystem fOWSystem;
    private Camera camera;
    private Matrix4x4 inverseMVP;
    private Material material;

    

    // Use this for initialization
    void Start()
    {
        if (!SystemInfo.supportsImageEffects || !shader || !shader.isSupported)
        {
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnEnable()
    {
        camera = GetComponent<Camera>();
        camera.depthTextureMode = DepthTextureMode.Depth;
        if (!shader)
            shader = Shader.Find("Image Effects/Fog of War");
    }

    void OnDisable()
    {
        if (material)
            DestroyImmediate(material);
    }

    void OnRenderImage(RenderTexture paramSource,RenderTexture paramDestination)
    {
        if (!fOWSystem)
        {
            fOWSystem = HiFowSystem.Instance;
            if (!fOWSystem)
                fOWSystem = FindObjectOfType<HiFowSystem>();
        }
        if (!fOWSystem || !fOWSystem.enabled)
        {
            enabled = false;
            return;
        }

        inverseMVP = (camera.projectionMatrix * camera.worldToCameraMatrix).inverse;

        float invScale = 1f/fOWSystem.worldSize;
        Transform t = fOWSystem.transform;
        float x = t.position.x - fOWSystem.worldSize*0.5f;
        float z = t.position.z - fOWSystem.worldSize*0.5f;

        if (material == null)
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.HideAndDontSave;
        }

        Vector4 camPos = transform.position;
        Vector4 p = new Vector4(-x * invScale, -z * invScale, invScale, 1);
        material.SetColor("_Unexplored", unexplored);
        material.SetColor("_Explored", explored);
        material.SetVector("_CamPos", camPos);
        material.SetVector("_Params", p);
        material.SetMatrix("_InverseMVP", inverseMVP);
        material.SetTexture("_FogTex0", fOWSystem.texture2D0);
        material.SetTexture("_FogTex1", fOWSystem.texture2D1);

        Graphics.Blit(paramSource, paramDestination, material);
    }
}
