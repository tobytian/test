//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("input system")
    {
        Add(new EmitInputSystem(contexts));
        Add(new CreateMoverSystem(contexts));
        Add(new CommandMoveSystem(contexts));
    }
}