//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public class GameController : MonoBehaviour
{
    private Systems _systems;

    private Contexts _contexts;
    // Use this for initialization
    void Start()
    {
        _contexts = Contexts.sharedInstance;
        _systems = CreateSystems(_contexts);
        _systems.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    static Systems CreateSystems(Contexts contexts)
    {
        return new Feature("systems")
            .Add(new InputSystems(contexts))
            .Add(new MovementSystems(contexts))
            .Add(new ViewSystems(contexts));
    }
}
