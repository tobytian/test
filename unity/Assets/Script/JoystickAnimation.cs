//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using UnityEngine;
public class JoystickAnimation : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    [SerializeField]
    private float waggleRange = 30;//摇杆晃动幅度
    [SerializeField]
    private float waggleSpeed = 2;

    private Animator animator;
    private string _horizontal = "horizontal";
    private string _vertical = "vertical";
    // Use this for initialization
    void Start()
    {
        joystick.DraggingHandler += OnDraggingEvent;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        OnDragEnd();
    }

    void OnDraggingEvent(Vector3 v)
    {
        dragOffset = v;
        float rate = 1 / 0.7f;
        v *= rate;
        animator.SetFloat(_horizontal, v.x);
        animator.SetFloat(_vertical, v.y);
    }

    void OnDragEnd()
    {
        if (joystick != null && joystick.State == Joystick.EState.End)
        {
            animator.SetFloat(_horizontal, 0);
            animator.SetFloat(_vertical, 0);
            transform.eulerAngles = Vector3.zero;
        }
    }


    private Vector3 dragOffset;
    float x, y;//旋转偏移值
    private Vector3 waggleV = Vector3.zero;
    void Rotate()
    {
        if (joystick != null && joystick.State == Joystick.EState.Dragging)
        {
            if (dragOffset.y > 0 && x < waggleRange)
            {
                x += dragOffset.y * waggleSpeed;
            }
            if (dragOffset.y < 0 && x > -waggleRange)
            {
                x += dragOffset.y * waggleSpeed;
            }

            if (dragOffset.x > 0 && y < waggleRange)
            {
                y += dragOffset.x * waggleSpeed;
            }
            if (dragOffset.x < 0 && y > -waggleRange)
            {
                y += dragOffset.x * waggleSpeed;
            }
            waggleV.x = x;
            waggleV.y = -y;
            transform.eulerAngles = waggleV;
        }
    }
}
