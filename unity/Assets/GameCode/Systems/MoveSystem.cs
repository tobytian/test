//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class MoveSystem : IExecuteSystem, ICleanupSystem
{
    private readonly IGroup<GameEntity> _moves;
    private readonly IGroup<GameEntity> _moveCompletes;
    private const float _speed = 4f;

    public MoveSystem(Contexts contexts)
    {
        _moves = contexts.game.GetGroup(GameMatcher.Move);
        _moveCompletes = contexts.game.GetGroup(GameMatcher.MoveComplete);
    }

    public void Execute()
    {
        foreach (var VARIABLE in _moves.GetEntities())
        {
            Vector2 dir = VARIABLE.move.target - VARIABLE.position.value;
            Vector2 newPosition = VARIABLE.position.value + dir.normalized * _speed * Time.deltaTime;
            VARIABLE.ReplacePosition(newPosition);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            VARIABLE.ReplaceDirection(angle);

            float dist = dir.magnitude;
            if (dist <= 0.5f)
            {
                VARIABLE.RemoveMove();
                VARIABLE.isMoveComplete = true;
            }
        }
    }

    public void Cleanup()
    {
        foreach (var VARIABLE in _moveCompletes.GetEntities())
        {
            VARIABLE.isMoveComplete = true;
        }
    }
}