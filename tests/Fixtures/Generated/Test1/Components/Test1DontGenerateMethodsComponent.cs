//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class Test1Matcher {

    static Entitas.IMatcher<Test1Entity> _matcherDontGenerateMethods;

    public static Entitas.IMatcher<Test1Entity> DontGenerateMethods {
        get {
            if (_matcherDontGenerateMethods == null) {
                var matcher = (Entitas.Matcher<Test1Entity>)Entitas.Matcher<Test1Entity>.AllOf(Test1ComponentsLookup.DontGenerateMethods);
                matcher.ComponentNames = Test1ComponentsLookup.componentNames;
                _matcherDontGenerateMethods = matcher;
            }

            return _matcherDontGenerateMethods;
        }
    }
}
