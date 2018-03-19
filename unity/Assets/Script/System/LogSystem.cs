//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class TestLogSystem:ReactiveSystem<GameEntity>
{
    public TestLogSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
         return context.CreateCollector(GameMatcher.Hp);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHp;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var VARIABLE in entities)
        {
            Debug.Log(VARIABLE.hp.hp);
        }
    }
}
