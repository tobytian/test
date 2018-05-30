using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class Server : MonoBehaviour
{
    public string Ip = "127.0.0.1";

    public int Port = 7777;
    // Use this for initialization
    void Start()
    {
        Type type = this.GetType();
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        for (int i = 0; i < methods.Length; i++)
        {
            var attris = methods[i].GetCustomAttributes(true);
            for (int j = 0; j < attris.Length; j++)
            {
                var attri = attris[j] as MyAttribute;
                if (attri != null)
                {
                    Debug.Log(attri.Name);
                }
            }
        }


        return;
        Init();
    }

    [My("testmethod")]
    void Method()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Init()
    {
        MasterServer.ipAddress = Ip;
        MasterServer.port = Port;
        MasterServer.RegisterHost("gameTypeName", "gameName");

    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        Debug.Log(msEvent);
        if (msEvent == MasterServerEvent.RegistrationSucceeded)
            Debug.Log("Server registered");

    }
}

public class MyAttribute : Attribute
{
    public string Name;
    public MyAttribute(string name)
    {
        Name = name;
    }
}