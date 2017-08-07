using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestEditor
{
    [MenuItem("Test/log")]
    public static void Test()
    {
        Debug.Log("from editor");
        islog = false;
    }
    private static bool islog = true;
    [MenuItem("Test/log", true)]
    public static bool Test2()
    {
        return islog;
    }
}