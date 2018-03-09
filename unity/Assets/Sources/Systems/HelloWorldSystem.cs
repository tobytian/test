//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class HelloWorldSystem : IInitializeSystem
{
    private readonly GameContext _context;

    public HelloWorldSystem(Contexts contexts)
    {
        _context = contexts.game;
    }
    public void Initialize()
    {
        _context.CreateEntity().AddDebugMessage("hello world");
    }
}
