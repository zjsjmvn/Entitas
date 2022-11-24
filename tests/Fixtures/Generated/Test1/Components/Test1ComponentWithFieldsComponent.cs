//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test1Entity {

    public ComponentWithFields componentWithFields { get { return (ComponentWithFields)GetComponent(Test1ComponentsLookup.ComponentWithFields); } }
    public bool hasComponentWithFields { get { return HasComponent(Test1ComponentsLookup.ComponentWithFields); } }

    public void AddComponentWithFields(string newPublicField) {
        var index = Test1ComponentsLookup.ComponentWithFields;
        var component = (ComponentWithFields)CreateComponent(index, typeof(ComponentWithFields));
        component.publicField = newPublicField;
        AddComponent(index, component);
    }

    public void ReplaceComponentWithFields(string newPublicField) {
        var index = Test1ComponentsLookup.ComponentWithFields;
        var component = (ComponentWithFields)CreateComponent(index, typeof(ComponentWithFields));
        component.publicField = newPublicField;
        ReplaceComponent(index, component);
    }

    public void RemoveComponentWithFields() {
        RemoveComponent(Test1ComponentsLookup.ComponentWithFields);
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

    static Entitas.IMatcher<Test1Entity> _matcherComponentWithFields;

    public static Entitas.IMatcher<Test1Entity> ComponentWithFields {
        get {
            if (_matcherComponentWithFields == null) {
                var matcher = (Entitas.Matcher<Test1Entity>)Entitas.Matcher<Test1Entity>.AllOf(Test1ComponentsLookup.ComponentWithFields);
                matcher.ComponentNames = Test1ComponentsLookup.componentNames;
                _matcherComponentWithFields = matcher;
            }

            return _matcherComponentWithFields;
        }
    }
}
