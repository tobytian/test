//****************************************************************************
// Description:
// Author: hiramtan@qq.com
//***************************************************************************

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Ping = UnityEngine.Ping;

public class NewBehaviourScript : MonoBehaviour
{
    private string ip = "122.11.58.160";
    private int pingTime;
    private Ping p;
    private float timeOut = 1;
    private float lastTime;
    void Start()
    {
        StartCoroutine(Ping());
    }
    IEnumerator Ping()
    {
        p = new Ping("127.0.0.1");
        lastTime = Time.realtimeSinceStartup;
        while (!p.isDone && Time.realtimeSinceStartup - lastTime < 1)
        {
            yield return null;
        }
        pingTime = p.time;
        p.DestroyPing();
        yield return new WaitForSeconds(1);//一秒更新一次
        StartCoroutine(Ping());
    }
}
