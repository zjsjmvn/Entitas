﻿using System.Collections.Generic;
using Jenny;
using DesperateDevs.Serialization;

namespace Entitas.Plugins
{
    public abstract class AbstractGenerator : ICodeGenerator, IConfigurable
    {
        public abstract string Name { get; }
        public int Order => 0;
        public bool RunInDryMode => true;

        public Dictionary<string, string> DefaultProperties => _ignoreNamespacesConfig.DefaultProperties;

        readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new IgnoreNamespacesConfig();

        public void Configure(Preferences preferences)
        {
            _ignoreNamespacesConfig.Configure(preferences);
            CodeGeneratorExtensions.IgnoreNamespaces = _ignoreNamespacesConfig.IgnoreNamespaces;
        }

        public abstract CodeGenFile[] Generate(CodeGeneratorData[] data);
    }
}
