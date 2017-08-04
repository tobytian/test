using System;
using System.Collections;
using UnityEngine;


using Object = UnityEngine.Object;


public class Test : MonoBehaviour
{
    void Start()
    {
        var test = new TestLoad("Cube");
        test.SetAction((Object) =>
        {
            Instantiate(Object);
        });

        StartCoroutine(test);

    }





    //void Start()
    //{
    //    TestAction(delegate ()
    //    {
    //        Debug.Log("finish");
    //    });
    //}
    //void TestAction(Action param)
    //{
    //    param();
    //}
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
