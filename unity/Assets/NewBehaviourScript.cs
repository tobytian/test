//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    public Test test1;
    [SerializeField]
    public Test test2;
    void Start()
    {
        int test=0;

        int a = 1;
        for (int i = 100; i != 999; i++)
        {
            a += i;


            test = i;
        }
        Debug.LogError(test);


        return;
        test1 = new Test();
        test1.x = 10;
        test1.Testttt = new Testttt();
        test1.Testttt.x = 10;

        test2 = (Test)test1.Clone();
        test2.x = 20;
        test2.Testttt.x = 20;
    }
    [System.Serializable]
    public class Test
    {
        public int x;
        public Testttt Testttt;
        public Test Clone()
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            return formatter.Deserialize(stream) as Test;
        }
    }
    [System.Serializable]
    public class Testttt
    {
        public int x;
    }
}
