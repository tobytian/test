//****************************************************************************
// Description:
// Author: hiramtan@qq.com
//***************************************************************************

using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuild : MonoBehaviour
{
    [MenuItem("Jenkins/Build")]
    private static void Build()
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        var Array = EditorBuildSettings.scenes;
        BuildPipeline.BuildPlayer(Array, "./test.apk", BuildTarget.Android, BuildOptions.None);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
