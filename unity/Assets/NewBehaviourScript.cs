//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    private int count = 30;

    public bool isStart;

    void Start()
    {
        //for (int i = 0; i < count; i++)
        //{
        //    Instantiate(Resources.Load("go"));
        //}
    }

    private int t = 0;
    void Update()
    {

        if (!isStart)
            return;
        if (t > count)
            return;
        t++;
        Instantiate(Resources.Load("go"));
    }
}
