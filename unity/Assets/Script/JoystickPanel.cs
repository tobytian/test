//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using UnityEngine;
public class JoystickPanel : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    private Animator animator;
    private string _horizontal = "horizontal";
    private string _vertical = "vertical";
    // Use this for initialization
    void Start()
    {
        joystick.DraggingHandler += OnDraggingEvent;
        joystick.StateChangeHandler += OnStateChangeEvent;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }

    void OnDraggingEvent(Vector3 v)
    {
        dragOffset = v;
        float rate = 1 / 0.7f;
        v *= rate;
        animator.SetFloat(_horizontal, v.x);
        animator.SetFloat(_vertical, v.y);
    }

    void OnStateChangeEvent(Joystick.EState state)
    {
        if (state == Joystick.EState.End)
        {
            animator.SetFloat(_horizontal, 0);
            animator.SetFloat(_vertical, 0);
            waggleV = Vector3.zero;
        }
    }

    public float waggleRange = 30;//摇杆晃动幅度
    public float waggleSpeed = 2;
    private Vector3 dragOffset;

    float x, y;
    private Vector3 waggleV = Vector3.zero;
    void Rotate()
    {
        if (dragOffset.y > 0 && x < waggleRange)
        {
            x += dragOffset.y* waggleSpeed;
        }
        if (dragOffset.y < 0 && x > -waggleRange)
        {
            x += dragOffset.y* waggleSpeed;
        }

        if (dragOffset.x > 0 && y < waggleRange)
        {
            y += dragOffset.x* waggleSpeed;
        }
        if (dragOffset.x < 0 && y > -waggleRange)
        {
            y += dragOffset.x* waggleSpeed;
        }
        waggleV.x = x;
        waggleV.y = -y;
        transform.eulerAngles = waggleV; ;
    }
}
