//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test1Entity {

    public AnyUniqueEventListenerComponent anyUniqueEventListener { get { return (AnyUniqueEventListenerComponent)GetComponent(Test1ComponentsLookup.AnyUniqueEventListener); } }
    public bool hasAnyUniqueEventListener { get { return HasComponent(Test1ComponentsLookup.AnyUniqueEventListener); } }

    public void AddAnyUniqueEventListener(System.Collections.Generic.List<IAnyUniqueEventListener> newValue) {
        var index = Test1ComponentsLookup.AnyUniqueEventListener;
        var component = (AnyUniqueEventListenerComponent)CreateComponent(index, typeof(AnyUniqueEventListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAnyUniqueEventListener(System.Collections.Generic.List<IAnyUniqueEventListener> newValue) {
        var index = Test1ComponentsLookup.AnyUniqueEventListener;
        var component = (AnyUniqueEventListenerComponent)CreateComponent(index, typeof(AnyUniqueEventListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAnyUniqueEventListener() {
        RemoveComponent(Test1ComponentsLookup.AnyUniqueEventListener);
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

    static Entitas.IMatcher<Test1Entity> _matcherAnyUniqueEventListener;

    public static Entitas.IMatcher<Test1Entity> AnyUniqueEventListener {
        get {
            if (_matcherAnyUniqueEventListener == null) {
                var matcher = (Entitas.Matcher<Test1Entity>)Entitas.Matcher<Test1Entity>.AllOf(Test1ComponentsLookup.AnyUniqueEventListener);
                matcher.ComponentNames = Test1ComponentsLookup.componentNames;
                _matcherAnyUniqueEventListener = matcher;
            }

            return _matcherAnyUniqueEventListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class Test1Entity {

    public void AddAnyUniqueEventListener(IAnyUniqueEventListener value) {
        var listeners = hasAnyUniqueEventListener
            ? anyUniqueEventListener.value
            : new System.Collections.Generic.List<IAnyUniqueEventListener>();
        listeners.Add(value);
        ReplaceAnyUniqueEventListener(listeners);
    }

    public void RemoveAnyUniqueEventListener(IAnyUniqueEventListener value, bool removeComponentWhenEmpty = true) {
        var listeners = anyUniqueEventListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAnyUniqueEventListener();
        } else {
            ReplaceAnyUniqueEventListener(listeners);
        }
    }
}
