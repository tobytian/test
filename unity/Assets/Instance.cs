//****************************************************************************
// Description:
// Author: hiramtan@qq.com
//***************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var t = typeof(Test);

        Debug.Log(t);

        var tt = Activator.CreateInstance(t);
        var ttt = tt as Test;
        ttt.L();
    }
}


public class Test
{
    public void L()
    {
        Debug.Log("execute");
    }
}
