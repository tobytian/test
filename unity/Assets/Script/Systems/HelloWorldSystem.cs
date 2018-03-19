//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections.Generic;
using Entitas;
using UnityEngine;
public class HelloWorldSystem : ReactiveSystem<GameEntity>
{
    private Contexts _contexts;
    public HelloWorldSystem(IContext<GameEntity> context) : base(context)
    {
    }
    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.HelloWorld);
    }
    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHelloWorld;
    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            Debug.Log(e.helloWorld.s);
        }
    }
}