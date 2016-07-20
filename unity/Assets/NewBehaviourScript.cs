//*********************************************************************
// Description:
// Author: hiramtan@live.com
//*********************************************************************
using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public class NewBehaviourScript : MonoBehaviour
{


    public Texture2D texture;

    public Material mMat;


    private int size = 128;
    // Use this for initialization
    void Start()
    {
        texture = new Texture2D(128,128);

        var test = size * size;

        Color32[] colors = new Color32[test];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.red;
        }

        texture.SetPixels32(colors);
        texture.Apply();



        Shader shader = new Shader();
        shader = Shader.Find("Standard");
        mMat = new Material(shader);

        mMat.SetTexture("_Albedo", texture);
        mMat.mainTexture = texture;

        mMat.color = Color.green;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        Graphics.Blit(source, destination);
    }
}
