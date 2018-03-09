//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CreateMoverSystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext _gameContext;

    public CreateMoverSystem(Contexts contexts) : base(contexts.input)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.RightMouse, InputMatcher.MouseDown));
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseDown;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var e in entities)
        {
            GameEntity mover = _gameContext.CreateEntity();
            mover.isMover = true;
            mover.AddPosition(e.mousePosition.position);
            mover.AddDirection(Random.Range(0, 360));
            mover.AddSprite("Bee");
        }
    }
}
