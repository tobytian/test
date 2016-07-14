//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debuger.fontSize = 20;
        Debuger.EnableOnScreen(true);



        var test = LightmapSettings.lightmaps;
        Debuger.Log(test.Length);

        LightmapData test2 = test[0];
        Debuger.Log(test2.lightmapFar.name);
        Debuger.Log(test2.lightmapNear.name);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
