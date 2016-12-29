using UnityEngine;
using Zenject;

public class test : MonoInstaller<test>
{
    public override void InstallBindings()
    {
        Container.Bind<string>().FromInstance("Hello World!");
        Container.Bind<TestRunner>().NonLazy();
    }
}
public class TestRunner
{
    public TestRunner(string message)
    {
        Debug.Log(message);
    }
}