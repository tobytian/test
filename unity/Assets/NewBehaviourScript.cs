using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        int[] test = new[] {1 };
        BitArray bitArray = new BitArray(test);

        int[] test1 = new[] { 2 };
        BitArray bitArray1 = new BitArray(test1);

        var result = bitArray.Or(bitArray1);

        string s = "";
        for (int i = result.Count; i > 0; i--)
        {
            var value = result[i - 1] ? 1 : 0;
            s += value;
        }
        Debug.Log(s);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
