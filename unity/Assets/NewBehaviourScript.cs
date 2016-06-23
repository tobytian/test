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





        Bezier bezier = new Bezier(Vector3.left*10, Vector3.up*10,Vector3.down*10, Vector3.right *10);
        lr.SetVertexCount(100);


        for (int i = 1; i <= 100; i++)
        {
            Vector3 vec = bezier.GetPointAtTime((float)(i * 0.01));
            lr.SetPosition(i - 1, vec);
        }
















        //int radius = 30;
        //int count = 100;
        //float eachAngle = 360f/count;
        //Vector3 forward = transform.forward;
        //lr.SetVertexCount(count+1);
        //for (int i = 0; i <= count; i++)
        //{
        //    Vector3 pos = Quaternion.Euler(0, eachAngle*i, 0)*forward*radius + transform.position;
        //    lr.SetPosition(i,pos);
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }
}
