//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CreateSystem : IInitializeSystem
{
    private Contexts _contexts;
    public CreateSystem(Contexts contexts)
    {
        _contexts = contexts;
    }

    public void Initialize()
    {
       var e= _contexts.game.CreateEntity();
        e.AddHp(100);
    }
}