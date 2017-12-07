//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using UnityEngine;
using UnityEngine.EventSystems;

public class DragObject : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;
    // Use this for initialization
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnDrag(PointerEventData data)
    {
        //Debug.LogError(Input.mousePosition);


        transform.position = Input.mousePosition;


        return;

        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out localpoint);

        Vector2 normalizedPoint = Rect.PointToNormalized(rectTransform.rect, localpoint);

        Debug.Log(normalizedPoint);



        rectTransform.position = normalizedPoint;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
    }
}
