//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CoolNameComponent coolName { get { return (CoolNameComponent)GetComponent(GameComponentsLookup.CoolName); } }
    public bool hasCoolName { get { return HasComponent(GameComponentsLookup.CoolName); } }

    public void AddCoolName(BadName newValue) {
        var index = GameComponentsLookup.CoolName;
        var component = CreateComponent<CoolNameComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCoolName(BadName newValue) {
        var index = GameComponentsLookup.CoolName;
        var component = CreateComponent<CoolNameComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCoolName() {
        RemoveComponent(GameComponentsLookup.CoolName);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherCoolName;

    public static Entitas.IMatcher<GameEntity> CoolName {
        get {
            if (_matcherCoolName == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CoolName);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCoolName = matcher;
            }

            return _matcherCoolName;
        }
    }
}
