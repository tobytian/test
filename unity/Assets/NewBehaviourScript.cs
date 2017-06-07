//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public List<GameObject> test = new List<GameObject>();


    public List<GameObject> test1;


    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 100, 50), "click"))
            test.RemoveAt(0);
    }

    // Use this for initialization
    void Start()
    {
        test.Add(new GameObject());
        test.Add(new GameObject());
        test.Add(new GameObject());


        test1 = new List<GameObject>(test);

        // var test

    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogError(test.Count + "---" + test1.Count);
    }
}
