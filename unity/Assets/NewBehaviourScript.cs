using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        var x = 1;
        var y = 2;
        var z = x | y;
        Debug.Log(z);

        return;
        int[] test = new[] { 0, 0 };
        BitArray bitArray = new BitArray(test);
        Debug.Log(bitArray.Count);
        string s = "";
        for (int i = bitArray.Count; i > 0; i--)
        {
            var value = bitArray[i - 1] ? 1 : 0;
            s += value;
        }
        Debug.Log(s);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
