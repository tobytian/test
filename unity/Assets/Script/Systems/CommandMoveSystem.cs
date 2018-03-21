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
        foreach (InputEntity e in entities)
        {
            GameEntity[] movers = _movers.GetEntities();
            if (movers.Length <= 0) return;

            GameEntity mover = movers[Random.Range(0, movers.Length)];
            if (mover.hasMove)
            {
                mover.ReplaceMove(e.mouseDown.postion);
            }
            else
            {
                mover.AddMove(e.mouseDown.postion);
            }
        }
    }
}
