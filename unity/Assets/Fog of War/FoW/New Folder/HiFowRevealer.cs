//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class HiFowRevealer : MonoBehaviour
{

    public float range = 50;

    public bool isActive = true;

    private HiFowSystem.Revealer revealer;

    void Awake()
    {
        revealer = HiFowSystem.CreateRevealer();
    }

    void OnDisable()
    {
        revealer.isActive = false;
    }

    void OnDestroy()
    {
        HiFowSystem.DeleteRevealer(revealer);
        revealer = null;
    }

    void LateUpdate()
    {
        if (isActive)
        {
            revealer.pos = transform.position;
            revealer.range = range;
            revealer.isActive = true;
        }
        else
        {
            revealer.isActive = false;
        }
        revealer.cachedBuffer = null;
    }

    public void Rebuild()
    {
        revealer.cachedBuffer = null;
    }
}
