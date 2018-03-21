/****************************************************************************
 * Description:
 *
 * Author: hiramtan@live.com
 ****************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Entitas;

public class CommandMoveSystem:ReactiveSystem<InputEntity>
{
    private GameContext _gameContext;
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
        foreach (var e in entities)
        {
            GameEntity[] mover = _movers.GetEntities();
            if (mover.Length <= 0) return;
            mover[Random.Range(0,mover.Length)].ReplaceMove(e.mouseDown.postion);

        }
    }
}
