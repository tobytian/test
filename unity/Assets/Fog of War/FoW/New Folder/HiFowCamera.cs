//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class HiFowCamera : MonoBehaviour
{
    public Color unexplored = Color.gray;
    //public Color explored = Color.gray;

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

        inverseMVP = (camera.projectionMatrix*camera.worldToCameraMatrix).inverse;
    }
}
