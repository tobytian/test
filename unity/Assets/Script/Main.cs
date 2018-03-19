//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var context = Contexts.sharedInstance.game;
        var e = context.CreateEntity();
        e.AddHp(100);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
