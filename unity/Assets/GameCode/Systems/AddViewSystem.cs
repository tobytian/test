//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class AddViewSystem : ReactiveSystem<GameEntity> {
readonly Transform _viewContainer = new GameObject("game views").transform;
    private readonly GameContext _context;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        _context = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Sprite);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasSprite && !entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var VARIABLE in entities)
        {
            GameObject go   = new GameObject("game view");
            go.transform.SetParent(_viewContainer,false);
            VARIABLE.AddView(go);
            go.Link(VARIABLE, _context);
        }
    }
}
