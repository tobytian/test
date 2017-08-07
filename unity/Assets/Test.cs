using System;
using System.Collections;
using UnityEngine;

using Object = UnityEngine.Object;


public class Test : MonoBehaviour
{
    private int i=1;


    void Start()
    {
        Debug.LogError("execute");

        _SearchValidTaskFrom(1);
    }

    private int _SearchValidTaskFrom(int task)
    {
        while (true)
        {
            if (i == 0)
            {
                return 0;
            }
            else if (i == 3)
            {
                return task;
            }

            i = 10;
        }
        Debug.Log("finish");
    }

}


public class LoadAsyn : AsynTask
{
    private ResourceRequest resourceRequest;

    public override void Execute()
    {

    }
}



public abstract class AsynTask : IEnumerator
{
    private Action<Object> action;
    private bool isFinish = false;
    private Object asynResult;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="param">执行完成的回调</param>
    public AsynTask(Action<Object> param = null)
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


    public void Finish(Object param = null)
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
