//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootSystem : Feature
{
    public RootSystem(Contexts contexts)
    {
        Add(new TestLogSystem(contexts.game));
        Add(new CreateSystem(contexts));
    }
}
