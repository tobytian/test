//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class RenderDirectionSystem : ReactiveSystem<GameEntity>
{
    private readonly GameContext _context;

    public RenderDirectionSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Direction);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasDirection && entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var VARIABLE in entities)
        {
            float ang = VARIABLE.direction.value;
            VARIABLE.view.gameObject.transform.rotation = Quaternion.AngleAxis(ang-90,Vector3.forward);
        }
    }
}
