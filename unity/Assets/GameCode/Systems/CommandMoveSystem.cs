//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CommandMoveSystem : ReactiveSystem<InputEntity>
{
    private readonly GameContext _gameContext;
    private IGroup<GameEntity> _movers;

    public CommandMoveSystem(Contexts contexts) : base(contexts.input)
    {
        _movers = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Mover).NoneOf(GameMatcher.Move));
    }

    protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
    {
        return context.CreateCollector(InputMatcher.AllOf(InputMatcher.LeftMouse, InputMatcher.MouseDown));
    }

    protected override bool Filter(InputEntity entity)
    {
        return entity.hasMouseDown;
    }

    protected override void Execute(List<InputEntity> entities)
    {
        foreach (var VARIABLE in entities)
        {
            GameEntity[] movers = _movers.GetEntities();
            if (movers.Length <= 0) return;
            movers[Random.Range(0, movers.Length)].ReplaceMove(VARIABLE.mouseDown.position);
        }
    }
}