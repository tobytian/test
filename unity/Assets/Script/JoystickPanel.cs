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

    void OnDraggingEvent(Vector2 v)
    {
        animator.SetFloat("x", v.x);
        animator.SetFloat("y", v.y);

        Debug.LogError(v);
    }

    void OnStateChangeEvent(Joystick.EState state)
    {
        if (state == Joystick.EState.End)
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 0);
        }
    }
}
