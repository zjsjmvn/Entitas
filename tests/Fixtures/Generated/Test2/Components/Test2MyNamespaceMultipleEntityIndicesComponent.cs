//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test2Entity {

    public My.Namespace.MultipleEntityIndexesComponent myNamespaceMultipleEntityIndexes { get { return (My.Namespace.MultipleEntityIndexesComponent)GetComponent(Test2ComponentsLookup.MyNamespaceMultipleEntityIndexes); } }
    public bool hasMyNamespaceMultipleEntityIndexes { get { return HasComponent(Test2ComponentsLookup.MyNamespaceMultipleEntityIndexes); } }

    public void AddMyNamespaceMultipleEntityIndexes(string newValue, string newValue2) {
        var index = Test2ComponentsLookup.MyNamespaceMultipleEntityIndexes;
        var component = (My.Namespace.MultipleEntityIndexesComponent)CreateComponent(index, typeof(My.Namespace.MultipleEntityIndexesComponent));
        component.value = newValue;
        component.value2 = newValue2;
        AddComponent(index, component);
    }

    public void ReplaceMyNamespaceMultipleEntityIndexes(string newValue, string newValue2) {
        var index = Test2ComponentsLookup.MyNamespaceMultipleEntityIndexes;
        var component = (My.Namespace.MultipleEntityIndexesComponent)CreateComponent(index, typeof(My.Namespace.MultipleEntityIndexesComponent));
        component.value = newValue;
        component.value2 = newValue2;
        ReplaceComponent(index, component);
    }

    public void RemoveMyNamespaceMultipleEntityIndexes() {
        RemoveComponent(Test2ComponentsLookup.MyNamespaceMultipleEntityIndexes);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiInterfaceGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test2Entity : IMyNamespaceMultipleEntityIndexesEntity { }

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class Test2Matcher {

    static Entitas.IMatcher<Test2Entity> _matcherMyNamespaceMultipleEntityIndexes;

    public static Entitas.IMatcher<Test2Entity> MyNamespaceMultipleEntityIndexes {
        get {
            if (_matcherMyNamespaceMultipleEntityIndexes == null) {
                var matcher = (Entitas.Matcher<Test2Entity>)Entitas.Matcher<Test2Entity>.AllOf(Test2ComponentsLookup.MyNamespaceMultipleEntityIndexes);
                matcher.ComponentNames = Test2ComponentsLookup.componentNames;
                _matcherMyNamespaceMultipleEntityIndexes = matcher;
            }

            return _matcherMyNamespaceMultipleEntityIndexes;
        }
    }
}
