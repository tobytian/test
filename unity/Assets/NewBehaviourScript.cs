//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    private LineRenderer lr;

    // Use this for initialization
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();


        int radius = 30;

        int count = 100;
        float eachAngle = 360f/count;
        Vector3 forward = transform.forward;

        lr.SetVertexCount(count+1);
        for (int i = 0; i <= count; i++)
        {
            Vector3 pos = Quaternion.Euler(0, eachAngle*i, 0)*forward*radius + transform.position;
            lr.SetPosition(i,pos);
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
