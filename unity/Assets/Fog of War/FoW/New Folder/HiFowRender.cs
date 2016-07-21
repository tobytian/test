﻿//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class HiFowRender : MonoBehaviour
{
    public float updateRate = 0.1f;
    private Renderer[] renderers;
    private float nextUpdate;
    public bool isVisible { get; private set; }
    private bool isCanUpdate = true;


    // Use this for initialization
	void Start ()
	{
	    renderers = GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Rebuild()
    {
        isCanUpdate = true;
    }

    void LateUpdate()
    {
        if (nextUpdate < Time.time)
            UpdateNow();
    }

    void UpdateNow()
    {
        nextUpdate = Time.time + updateRate;
        if (!HiFowSystem.Instance)
        {
            enabled = false;
            return;
        }
        bool temp = HiFowSystem.Instance.IsVisible(transform.position);
        if (isCanUpdate||isVisible!=temp)
        {
            isCanUpdate = false;
            isVisible = temp;
            for (int i = 0; i < renderers.Length; i++)
            {
                Renderer tempRender = renderers[i];
                if (tempRender)
                    tempRender.enabled = isVisible;
            }
            isCanUpdate = true;
            nextUpdate = Time.time;
        }
    }
}