//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{
    public Canvas myCanvas;

    public float x;//x轴倾斜
    public float y;//y轴倾斜
    private float _screenWidth, _screenHeight;
    private float _imageWidth, _imageHeight;
    // Use this for initialization
    void Start()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        _imageWidth = GetComponent<RectTransform>().sizeDelta.x;
        _imageHeight = GetComponent<RectTransform>().sizeDelta.y;
        Debug.LogError(_imageWidth);
        Debug.Log(_imageHeight);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Vector3 mousePosition;
    public void OnDrag(PointerEventData data)
    {
        //Debug.Log(Screen.width + "-" + Screen.height);
        //Debug.LogError(Input.mousePosition);
        //transform.position = Input.mousePosition;

        if (Input.mousePosition.x - _screenWidth / 2f < -_screenWidth / 2f)
            mousePosition.x = Input.mousePosition.x + _imageWidth / 2f;
        else if (Input.mousePosition.x + _screenWidth / 2f > _screenWidth / 2f)
            mousePosition.x = Input.mousePosition.x - _imageWidth / 2f;



        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);


        return;
        var xAngle = pos.x / (_screenWidth / 2f) * x;
        var yAngle = pos.y / (_screenHeight / 2) * y;
        transform.eulerAngles = new Vector3(-yAngle, xAngle, 0);
    }
}
