//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test1Entity {

    static readonly My.Namespace.MyNamespaceFlagComponent myNamespaceMyNamespaceFlagComponent = new My.Namespace.MyNamespaceFlagComponent();

    public bool isMyNamespaceMyNamespaceFlag {
        get { return HasComponent(Test1ComponentsLookup.MyNamespaceMyNamespaceFlag); }
        set {
            if (value != isMyNamespaceMyNamespaceFlag) {
                var index = Test1ComponentsLookup.MyNamespaceMyNamespaceFlag;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : myNamespaceMyNamespaceFlagComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class Test1Matcher {

    static Entitas.IMatcher<Test1Entity> _matcherMyNamespaceMyNamespaceFlag;

    public static Entitas.IMatcher<Test1Entity> MyNamespaceMyNamespaceFlag {
        get {
            if (_matcherMyNamespaceMyNamespaceFlag == null) {
                var matcher = (Entitas.Matcher<Test1Entity>)Entitas.Matcher<Test1Entity>.AllOf(Test1ComponentsLookup.MyNamespaceMyNamespaceFlag);
                matcher.ComponentNames = Test1ComponentsLookup.componentNames;
                _matcherMyNamespaceMyNamespaceFlag = matcher;
            }

            return _matcherMyNamespaceMyNamespaceFlag;
        }
    }
}
