﻿using UnityEngine;
using UnityEngine.UI;

public class ShadowScript : MonoBehaviour
{

    public GameObject display; //角色形象
    private GameObject ShadowCamera;
    private GameObject map;
    private RenderTexture mTex = null; //即为显示阴影的RenderTexutre
    public int AntiAliasing = 4;

    ///Transform child;
    // Use this for initialization
    void Start()
    {

        //child = this.transform.FindChild("qiangu1");

        map = transform.FindChild("map").gameObject;
        ShadowCamera = transform.FindChild("camera").gameObject;

        if (!display)
            display = this.transform.parent.gameObject;
        mTex = new RenderTexture(2000, 2000, 0);
        mTex.name = "Shadow" +GetInstanceID();

        Camera mCamera = ShadowCamera.GetComponent<Camera>();
        mCamera.cullingMask = GetLayerMask(display.gameObject.layer);
        mCamera.targetTexture = mTex;
    }

    public LayerMask GetLayerMask(int layer)
    {
        LayerMask mask = 0;
        mask |= 1 << layer;
        return mask;
    }
    // Update is called once per frame
    void Update()
    {
        if (display != null)
        {
            mTex.anisoLevel = AntiAliasing;
        }
        map.GetComponent<Renderer>().material.mainTexture = mTex;
    }
}