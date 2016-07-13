//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

    private int count=10;


    void Start()
    {
       // InvokeRepeating("Test", 0, 0.5f);
    }

    void Test()
    {
        for (int i = 0; i < count; i++)
            Instantiate(Resources.Load("go"));


      
    }

    void Update()
    {
        Test();
    }
}
