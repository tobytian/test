//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ViewSystems : Feature {
    public ViewSystems(Contexts contexts) : base("view system")
    {
        Add(new AddViewSystem(contexts));
        Add(new RenderSpriteSystem(contexts));
        Add(new RenderPositionSystem(contexts));
        Add(new RenderDirectionSystem(contexts));
    }
}
