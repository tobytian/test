//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class DebugMessageSystem : ReactiveSystem<GameEntity> {
    
 
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.DebugMessage);
    }

    protected override bool Filter(GameEntity entity)
    {
       return entity.hasDebugMessage;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var VARIABLE in entities)
        {
            Debug.Log(VARIABLE.debugMessage.message);
        }
    }

    public DebugMessageSystem(Contexts contexts) : base(contexts.game)
    {
        
    }
}
