//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class HiFowCamera : MonoBehaviour
{
    public Color unexplored = Color.gray;

    private Color explored;
    private Shader shader;
    private HiFowSystem fOWSystem;
    private Camera thisCamera;
    private Matrix4x4 inverseMVP;
    private Material material;


    void Start()
    {
        if (!SystemInfo.supportsImageEffects || !shader || !shader.isSupported)
        {
            enabled = false;
        }
    }
    void OnEnable()
    {
        thisCamera = GetComponent<Camera>();
        thisCamera.depthTextureMode = DepthTextureMode.Depth;
        if (!shader)
            shader = Shader.Find("Image Effects/Fog of War");
    }

    void OnDisable()
    {
        if (material)
            DestroyImmediate(material);
    }

    void OnRenderImage(RenderTexture paramSource, RenderTexture paramDestination)
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

        inverseMVP = (thisCamera.projectionMatrix * thisCamera.worldToCameraMatrix).inverse;

        float invScale = 1f / fOWSystem.worldSize;
        Transform t = fOWSystem.transform;
        float x = t.position.x - fOWSystem.worldSize * 0.5f;
        float z = t.position.z - fOWSystem.worldSize * 0.5f;

        if (material == null)
        {
            material = new Material(shader);
            material.hideFlags = HideFlags.HideAndDontSave;
        }




        Vector4 camPos = transform.position;



        //if (QualitySettings.antiAliasing > 0)
        //{
        //    RuntimePlatform pl = Application.platform;

        //    if (pl == RuntimePlatform.WindowsEditor ||
        //        pl == RuntimePlatform.WindowsPlayer ||
        //        pl == RuntimePlatform.WindowsWebPlayer)
        //    {
        //        camPos.w = 1f;
        //    }
        //}


    

        Vector4 p = new Vector4(-x * invScale, -z * invScale, invScale, fOWSystem.blendFactor);
        material.SetColor("_Unexplored", unexplored);
        explored = unexplored;
        material.SetColor("_Explored", explored);
        material.SetVector("_CamPos", camPos);
        material.SetVector("_Params", p);
        material.SetMatrix("_InverseMVP", inverseMVP);
        material.SetTexture("_FogTex0", fOWSystem.texture2D0);
        material.SetTexture("_FogTex1", fOWSystem.texture2D1);
        Graphics.Blit(paramSource, paramDestination, material);
    }
}
