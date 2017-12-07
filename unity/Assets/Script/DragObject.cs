//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{
    public Canvas myCanvas;
    [Range(0, 90)]//不可能射线垂直
    public float x;//x轴倾斜
    [Range(0, 90)]//不可能射线垂直
    public float y;//y轴倾斜

    private float _imageWidth, _imageHeight;
    // Use this for initialization
    void Start()
    {
        _imageWidth = GetComponent<RectTransform>().sizeDelta.x;
        _imageHeight = GetComponent<RectTransform>().sizeDelta.y;
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
        #region Move
        if (Input.mousePosition.x - _imageWidth / 2f < 0)//left
        {
            mousePosition.x = _imageWidth / 2f;
        }
        else if (Input.mousePosition.x + _imageWidth / 2f > Screen.width)
        {
            mousePosition.x = Screen.width - _imageWidth / 2f;
        }
        else
        {
            mousePosition.x = Input.mousePosition.x;
        }

        if (Input.mousePosition.y + _imageHeight / 2f > Screen.height) //up
        {
            mousePosition.y = Screen.height - _imageWidth / 2f;
        }
        else if (Input.mousePosition.y - _imageHeight / 2f < 0)
        {
            mousePosition.y = _imageHeight / 2f;
        }
        else
        {
            mousePosition.y = Input.mousePosition.y;
        }
        //mousePosition = Input.mousePosition;
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
        #endregion

        #region Rotate
        var xAngle = pos.x / (Screen.width / 2f) * x;
        var yAngle = pos.y / (Screen.height / 2) * y;
        transform.eulerAngles = new Vector3(-yAngle, xAngle, 0);
        #endregion
    }
}
