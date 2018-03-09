//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class LogMouseClickSystem : IExecuteSystem
{
    private readonly GameContext _context;

    public LogMouseClickSystem(Contexts contexts)
    {
        _context = contexts.game;
    }

    public void Execute()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _context.CreateEntity().AddDebugMessage("left");
        }
        if (Input.GetMouseButtonDown(1))
        {
            _context.CreateEntity().AddDebugMessage("right");
        }
    }
}
