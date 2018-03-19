//****************************************************************************
// Description:
// Author: hiramtan@live.com
//****************************************************************************

using Entitas;

public class RootSystems : Feature
{
    public RootSystems(Contexts contexts)
    {
        Add(new CreatSystem(contexts));
        Add(new HelloWorldSystem(contexts.game));
    }
}
