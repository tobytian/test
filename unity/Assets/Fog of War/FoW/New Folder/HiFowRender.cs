//*********************************************************************
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
    private bool isVisible;
    private bool isCanUpdate = true;
    // Use this for initialization
	void Start ()
	{
	    renderers = GetComponentsInChildren<Renderer>();
	}
    void LateUpdate()
    {
        if (nextUpdate < Time.time)
        {
            nextUpdate = Time.time + updateRate;
            if (!HiFowSystem.Instance)
            {
                enabled = false;
                return;
            }
            bool temp = HiFowSystem.Instance.IsVisible(transform.position);
            if (isCanUpdate || isVisible != temp)
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
}
