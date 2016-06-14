using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    public GameObject ShadowCamera;

   public GameObject map;

    public RenderTexture mTex = null; //即为显示阴影的RenderTexutre
   // public int AntiAliasing = 4;

    //Transform child;
    // Use this for initialization
    void Start()
    {

       // child = this.transform.FindChild("qiangu1");

        //map = child.FindChild("child/map").gameObject;
     

        //mTex = new RenderTexture(2000, 2000, 0);
        //mTex.name = "Shadow" + GetInstanceID();

        //Camera mCamera = ShadowCamera.GetComponent<Camera>();
       
        //mCamera.targetTexture = mTex;
    }

    //public LayerMask GetLayerMask(int layer)
    //{
    //    LayerMask mask = 0;
    //    mask |= 1 << layer;
    //    return mask;
    //}
    // Update is called once per frame
    void Update()
    {
        //if (display != null)
        //{
        //    mTex.anisoLevel = AntiAliasing;
        //}
//        map.GetComponent<Renderer>().maternTexture = mTex;
        map.GetComponent<Renderer>().material.mainTexture = mTex;
    }
}