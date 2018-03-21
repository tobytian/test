/****************************************************************************
 * Description:
 *
 * Author: hiramtan@live.com
 ****************************************************************************/
using UnityEngine;
using System.Collections;
using Entitas;

public class MoveSystem:IExecuteSystem,ICleanupSystem
{
    private IGroup<GameEntity> _moves;
    private IGroup<GameEntity> _moveCompletes;
    public MoveSystem(Contexts contexts)
    {
        _moves = contexts.game.GetGroup(GameMatcher.Move);
        _moveCompletes = contexts.game.GetGroup(GameMatcher.MoveComplete);
    }
    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Cleanup()
    {
        throw new System.NotImplementedException();
    }
}
