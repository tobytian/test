//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {



        var t = Activator.CreateInstance(typeof(Test4));
        Debug.Log(t);

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Test1
{
}

public class Test2
{
}

public class Test3
{

}

public class Test4
{
    public Test4()
    { }
    public Test4(Test1 t1, Test2 t2)
    {
        Debug.Log(t1);
    }
}
