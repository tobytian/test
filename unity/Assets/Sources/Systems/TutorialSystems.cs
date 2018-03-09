//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class TutorialSystems : Feature {

    public TutorialSystems(Contexts contexts) : base("Tutorial systems")
    {
        Add(new CleanupDebugMessageSystem(contexts));
        Add(new LogMouseClickSystem(contexts));
        Add(new DebugMessageSystem(contexts));
        Add(new HelloWorldSystem(contexts));
    }
}
