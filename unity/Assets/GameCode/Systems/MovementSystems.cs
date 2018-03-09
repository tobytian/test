//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class MovementSystems:Feature
{
    public MovementSystems(Contexts contexts) : base("movement system")
    {
        Add(new MoveSystem(contexts));
    }
}
