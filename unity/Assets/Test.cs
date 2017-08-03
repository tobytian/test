using System;
using System.Collections;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Action action;
    // Use this for initialization
    void Start()
    {
        myAsyn t = new myAsyn(Finish);
        t.Start();

    }

    void Finish(object p)
    {
        Debug.Log("finish");
    }
}


public class myAsyn : TestAsyn
{
    public myAsyn(Action<object> param = null) : base(param)
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
    public TestAsyn(Action<object> param = null)
    {
        action = param;
        isFinish = false;
    }

    public void Start()
    {
        GameObject.FindObjectOfType<Test>().StartCoroutine(this);
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
        //throw new System.NotImplementedException();
    }

    public object Current { get; private set; }
}
