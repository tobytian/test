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
        Debuger.Log("name: "+test2.lightmapFar.name);

       var test3= test2.lightmapFar;

        Debuger.Log("size: "+test3.width+"---"+test3.height);

        Debuger.Log(test3.texelSize);



    }

    // Update is called once per frame
    void Update()
    {

    }
}
