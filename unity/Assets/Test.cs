using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        TestAsyn test = new TestAsyn(Finish);
        StartCoroutine(test);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Finish()
    {
        Debug.LogError("finish");
    }
}

public class TestAsyn : IEnumerator
{
    private Action action;
    public TestAsyn(Action param)
    {
        action = param;
    }

    private int i = 0;

    public bool MoveNext()
    {
        i++;
        if (i < 100)
        {
            Debug.Log("execute");
            return true;
        }

        action();
        return false;
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public object Current { get; private set; }
}
