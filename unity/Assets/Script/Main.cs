/****************************************************************************
 * Description:
 *
 * Author: hiramtan@live.com
 ****************************************************************************/
using Entitas;
using UnityEngine;

public class Main : MonoBehaviour
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

    Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Systems")
            .Add(new InputSystems(contexts))
            .Add(new MovementSystems(contexts))
            .Add(new ViewSystems(contexts));
    }
}
