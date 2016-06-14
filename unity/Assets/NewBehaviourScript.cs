using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

    public RenderTexture t;

    void Update()
    {
       GetComponent<Renderer>().material.mainTexture = t;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, 100, 100), t);
    }
}