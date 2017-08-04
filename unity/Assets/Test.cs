using System;
using System.Collections;
using UnityEngine;


using Object = UnityEngine.Object;

public class Test : MonoBehaviour
{

    void Start()
    {
        var t = new TestLoad("Cube");
        t.SetAction(Finish);

        StartCoroutine(t);
    }

    void Finish(Object p)
    {
        Debug.Log("finish");
        Instantiate(p);
    }

    IEnumerator t()
    {
        var t = Resources.LoadAsync("");
        yield return t;



    }
}



public class TestLoad : IEnumerator
{
    private string path;

    private ResourceRequest asyncOperation;

    private Action<Object> action;
    public TestLoad(string path)
    {
        this.path = path;

        asyncOperation = Resources.LoadAsync(path);
    }

    public void SetAction(Action<Object> param)
    {
        action = param;
    }


    public TestLoad Continue(Func<TestLoad, Object> param)
    {
        return new TestLoad(path);
    }

    public void Finish(Object param)
    {
        action(param);
    }

    public bool MoveNext()
    {
        if (!asyncOperation.isDone)
            return true;

        Finish(asyncOperation.asset);
        return false;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public object Current { get; private set; }
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
