//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class EmitInputSystem : IInitializeSystem, IExecuteSystem
{
    private readonly InputContext _context;
    private InputEntity _leftMousEntity;
    private InputEntity _rightMouseEntity;

    public EmitInputSystem(Contexts contexts)
    {
        _context = contexts.input;
    }

    public void Initialize()
    {
        _context.isLeftMouse = true;
        _leftMousEntity = _context.leftMouseEntity;

        _context.isRightMouse = true;
        _rightMouseEntity = _context.rightMouseEntity;
    }

    public void Execute()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(0))
            _leftMousEntity.ReplaceMouseDown(mousePosition);
        if (Input.GetMouseButton(0))
            _leftMousEntity.ReplaceMousePosition(mousePosition);

        if (Input.GetMouseButtonUp(0))
            _leftMousEntity.ReplaceMouseUp(mousePosition);


        // right mouse button
        if (Input.GetMouseButtonDown(1))
            _rightMouseEntity.ReplaceMouseDown(mousePosition);

        if (Input.GetMouseButton(1))
            _rightMouseEntity.ReplaceMousePosition(mousePosition);

        if (Input.GetMouseButtonUp(1))
            _rightMouseEntity.ReplaceMouseUp(mousePosition);
    }
}
