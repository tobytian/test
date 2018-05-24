using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    void Start()
    {
        Debug.Log("start");
        var e = new Entity();
        e.InjectRPCHandler(new Test());
        e.OnRPC(0, "Die", 1, 2, "3");
    }

    public class Test
    {
        [NewBehaviourScript.RPCAttribute]
        void Die(int i, float f, string s)
        {
            Debug.Log(i + f + s);
        }
    }




    public class Entity
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        private readonly object locker = new object();
        /// <summary>
        /// 响应RPC方法
        /// </summary>
        private Dictionary<string, InjectInfo> InjectInfos = new Dictionary<string, InjectInfo>();

        /// <summary>
        /// RPC调用
        /// </summary>
        /// <param name="method">方法名</param>
        /// <param name="args">可变参数</param>
        public void RPC(string method, params object[] args)
        {
            lock (locker)
            {
                if (string.IsNullOrEmpty(method))
                {
                    throw new Exception("Method name is null or empty");
                }
                if (args.Length == 0)
                {
                    throw new Exception("Have no param");
                }
            }
        }

        /// <summary>
        /// 注入RPC处理器。
        /// </summary>
        /// <param name="handler">处理器</param>
        public void InjectRPCHandler(object handler)
        {
            lock (locker)
            {
                Type type = handler.GetType();
                var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                    .Where(m => m.GetCustomAttributes(typeof(RPCAttribute), true).Length > 0).ToArray();
                for (int i = 0; i < methods.Length; i++)
                {
                    var name = methods[i].Name;
                    if (InjectInfos.ContainsKey(name))
                    {
                        throw new ArgumentException(string.Format("Alread inject this method[{0}]", name));
                    }
                    InjectInfos.Add(name, new InjectInfo(handler, methods[i]));
                }
            }
        }

        /// <summary>
        /// 处理RPC消息
        /// </summary>
        /// <param name="tick">帧号</param>
        /// <param name="method">方法名</param>
        /// <param name="args">参数</param>
        public void OnRPC(uint tick, string method, params object[] args)
        {
            InjectInfo injectInfo;
            if (!InjectInfos.TryGetValue(method, out injectInfo))
            {
                throw new Exception(string.Format("Havent inject this method[{0}]", method));
            }
            injectInfo.MethodInfo.Invoke(injectInfo.Obj, args);
        }
    }

    /// <summary>
    /// 注入信息
    /// </summary>
    private class InjectInfo
    {
        /// <summary>
        /// 注入对象
        /// </summary>
        public readonly object Obj;

        /// <summary>
        /// rpc方法信息
        /// </summary>
        public readonly MethodInfo MethodInfo;

        /// <summary>
        /// 构造注入信息
        /// </summary>
        /// <param name="obj">注入对象</param>
        /// <param name="methodInfo">rpc方法</param>
        public InjectInfo(object obj, MethodInfo methodInfo)
        {
            this.Obj = obj;
            this.MethodInfo = methodInfo;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class RPCAttribute : Attribute
    {

    }
}
