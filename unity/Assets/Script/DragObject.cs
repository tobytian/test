//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    private RectTransform dragAreaInternal;

    private RectTransform dragObjectInternal;
    // Use this for initialization
    void Start()
    {
        //dragAreaInternal = GetComponent<RectTransform>();
        //dragObjectInternal = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private Vector2 originalLocalPointerPosition;
    private Vector3 originalPanelLocalPosition;
    public void OnDrag(PointerEventData data)
    {

        var v = Camera.main.WorldToScreenPoint(Input.mousePosition);

        Debug.LogError(Input.mousePosition);

        transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(v.x, v.y);

    }
    public void OnBeginDrag(PointerEventData data)
    {
        //originalPanelLocalPosition = dragObjectInternal.localPosition;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(dragAreaInternal, data.position, data.pressEventCamera, out originalLocalPointerPosition);
    }
}
