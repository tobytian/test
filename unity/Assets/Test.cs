using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        myAsyn test = new myAsyn(Finish);

        StartCoroutine(test);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Finish(object param)
    {
        Debug.LogError("finish");
    }
}


public class myAsyn : TestAsyn
{
    public myAsyn(Action<object> param) : base(param)
    {
    }

    private int i = 0;
    public override void Execute()
    {
        i++;
        Debug.Log(i);
        if (i > 100)
            Finish();
    }
}



public abstract class TestAsyn : IEnumerator
{
    private Action<object> action;
    private bool isFinish = false;
    private object asynResult;
    public TestAsyn(Action<object> param)
    {
        action = param;
        isFinish = false;
    }

    public void Start()
    {

    }

    public bool MoveNext()
    {
        if (!isFinish)
        {
            Execute();
            return true;
        }
        action(asynResult);
        return false;
    }
    public abstract void Execute();


    public void Finish(object param = null)
    {
        asynResult = param;
        isFinish = true;
    }
    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public object Current { get; private set; }
}
