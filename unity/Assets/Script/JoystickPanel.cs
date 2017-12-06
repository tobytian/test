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

    }

    void OnDraggingEvent(Vector3 v)
    {
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
        }
    }
}
