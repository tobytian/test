//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{

    public Canvas myCanvas;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDrag(PointerEventData data)
    {
        //Debug.LogError(Input.mousePosition);
        //transform.position = Input.mousePosition;


        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
        //transform.position = myCanvas.transform.TransformPoint(pos);



        Debug.LogError(pos);

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }
}
