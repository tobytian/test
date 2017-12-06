//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : ScrollRect
{
    public EState State { get; private set; }
    public event Action<Vector3> DraggingHandler;
    private float _recoverTime = 0.1f;//摇杆恢复时间
    private float _radius;
    private Vector3 _contentOffset = Vector3.zero;
    public enum EState
    {
        None,
        Start,//开始拖拽
        Dragging,//正在拖拽
        End,//结束拖拽
    }

    void Awake()
    {
        //inertia = false;
        //movementType = MovementType.Unrestricted;
        _radius = (transform as RectTransform).sizeDelta.x * 0.5f;
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        State = EState.Start;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        State = EState.End;
    }

    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
        State = EState.Dragging;
        ProcessDraggingHandler();
        var contentPostion = this.content.anchoredPosition;
        if (contentPostion.magnitude > _radius)
        {
            contentPostion = contentPostion.normalized * _radius;
            SetContentAnchoredPosition(contentPostion);
        }
    }
    void Update()
    {
        RecoverContent();
        UpdateContentOffset();
    }

    void RecoverContent()
    {
        if (State == EState.End)
        {
            if (HiFloat.IsEqual(content.localPosition.x, 0) && HiFloat.IsEqual(content.localPosition.y, 0))
            {
                State = EState.None;
                content.localPosition = Vector3.zero;
            }
            float x = Mathf.Lerp(content.localPosition.x, 0.0f, _recoverTime);
            float y = Mathf.Lerp(content.localPosition.y, 0.0f, _recoverTime);
            content.localPosition = new Vector3(x, y, content.localPosition.z);
        }
    }

    void UpdateContentOffset()
    {
        _contentOffset = content.localPosition / _radius;
    }

    void ProcessDraggingHandler()
    {
        if (DraggingHandler != null)
        {
            DraggingHandler(_contentOffset);
        }
    }
}
