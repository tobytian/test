using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    }
}


public class Bit
{
    private byte[] _bytes;
    private BitArray _bitArray;
    private const int BitsOfOneByte = 8;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="bitCount">how many bit</param>
    public Bit(int bitCount = BitsOfOneByte)
    {
        if (bitCount <= 0)
        {
            throw new Exception("bit count can not less or equal 0");
        }
        var @base = bitCount / BitsOfOneByte;
        var left = bitCount % BitsOfOneByte;
        var length = left == 0 ? @base : @base + 1;
        if (length <= 0)
        {
            throw new Exception("byte array can not less or equal 0");
        }
        _bytes = new byte[length];
        _bytes[0] = 1;
        _bitArray = new BitArray(_bytes);
    }

    /// <summary>
    /// 向左移位
    /// </summary>
    /// <param name="count"></param>
    public void MoveLeft(int count)
    {
        var index = count / BitsOfOneByte;

        BitArray b = new BitArray(_bitArray.Length);

    }

    public string GetBit()
    {
        string s = "";
        for (int i = _bitArray.Length; i > 0; i--)
        {
            var value = _bitArray[i - 1] ? 1 : 0;
            s += value;
        }
        return s;
    }
}
