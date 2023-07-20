using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.Reflection;
using UnityEditor;
using UnityEngine;

namespace Entitas.Unity.Editor
{
    public static partial class EntityDrawer
    {
        static Dictionary<string, bool[]> _contextToUnfoldedComponents;

        public static Dictionary<string, bool[]> contextToUnfoldedComponents =>
            _contextToUnfoldedComponents ?? (_contextToUnfoldedComponents = new Dictionary<string, bool[]>());

        static Dictionary<string, string[]> _contextToComponentMemberSearch;

        public static Dictionary<string, string[]> contextToComponentMemberSearch =>
            _contextToComponentMemberSearch ?? (_contextToComponentMemberSearch = new Dictionary<string, string[]>());

        static Dictionary<string, GUIStyle[]> _contextToColoredBoxStyles;

        public static Dictionary<string, GUIStyle[]> contextToColoredBoxStyles =>
            _contextToColoredBoxStyles ?? (_contextToColoredBoxStyles = new Dictionary<string, GUIStyle[]>());

        public struct ComponentInfo
        {
            public int index;
            public string name;
            public Type type;
        }

        static Dictionary<string, ComponentInfo[]> _contextToComponentInfos;

        public static Dictionary<string, ComponentInfo[]> contextToComponentInfos =>
            _contextToComponentInfos ?? (_contextToComponentInfos = new Dictionary<string, ComponentInfo[]>());

        static GUIStyle _foldoutStyle;

        public static GUIStyle foldoutStyle
        {
            get
            {
                if (_foldoutStyle == null)
                {
                    _foldoutStyle = new GUIStyle(EditorStyles.foldout);
                    _foldoutStyle.fontStyle = FontStyle.Bold;
                }

                return _foldoutStyle;
            }
        }

        static string _componentNameSearchString;

        public static string componentNameSearchString
        {
            get => _componentNameSearchString ?? (_componentNameSearchString = string.Empty);
            set => _componentNameSearchString = value;
        }

        public static readonly IDefaultInstanceCreator[] _defaultInstanceCreators;
        public static readonly ITypeDrawer[] _typeDrawers;
        public static readonly IComponentDrawer[] _componentDrawers;

        static EntityDrawer()
        {
            _defaultInstanceCreators = AppDomain.CurrentDomain.GetInstancesOf<IDefaultInstanceCreator>().ToArray();
            _typeDrawers = AppDomain.CurrentDomain.GetInstancesOf<ITypeDrawer>().ToArray();
            _componentDrawers = AppDomain.CurrentDomain.GetInstancesOf<IComponentDrawer>().ToArray();
        }

        static bool[] getUnfoldedComponents(IEntity entity)
        {
            if (!contextToUnfoldedComponents.TryGetValue(entity.contextInfo.Name, out var unfoldedComponents))
            {
                unfoldedComponents = new bool[entity.totalComponents];
                for (var i = 0; i < unfoldedComponents.Length; i++)
                    unfoldedComponents[i] = true;

                contextToUnfoldedComponents.Add(entity.contextInfo.Name, unfoldedComponents);
            }

            return unfoldedComponents;
        }

        static string[] getComponentMemberSearch(IEntity entity)
        {
            if (!contextToComponentMemberSearch.TryGetValue(entity.contextInfo.Name, out var componentMemberSearch))
            {
                componentMemberSearch = new string[entity.totalComponents];
                for (var i = 0; i < componentMemberSearch.Length; i++)
                    componentMemberSearch[i] = string.Empty;

                contextToComponentMemberSearch.Add(entity.contextInfo.Name, componentMemberSearch);
            }

            return componentMemberSearch;
        }

        static ComponentInfo[] getComponentInfos(IEntity entity)
        {
            if (!contextToComponentInfos.TryGetValue(entity.contextInfo.Name, out var infos))
            {
                var contextInfo = entity.contextInfo;
                var infosList = new List<ComponentInfo>(contextInfo.ComponentTypes.Length);
                for (var i = 0; i < contextInfo.ComponentTypes.Length; i++)
                {
                    infosList.Add(new ComponentInfo
                    {
                        index = i,
                        name = contextInfo.ComponentNames[i],
                        type = contextInfo.ComponentTypes[i]
                    });
                }

                infos = infosList.ToArray();
                contextToComponentInfos.Add(entity.contextInfo.Name, infos);
            }

            return infos;
        }

        static GUIStyle getColoredBoxStyle(IEntity entity, int index)
        {
            if (!contextToColoredBoxStyles.TryGetValue(entity.contextInfo.Name, out var styles))
            {
                styles = new GUIStyle[entity.totalComponents];
                for (var i = 0; i < styles.Length; i++)
                {
                    var hue = (float)i / (float)entity.totalComponents;
                    var componentColor = Color.HSVToRGB(hue, 0.7f, 1f);
                    componentColor.a = 0.15f;
                    var style = new GUIStyle(GUI.skin.box);
                    style.normal.background = createTexture(2, 2, componentColor);
                    styles[i] = style;
                }

                contextToColoredBoxStyles.Add(entity.contextInfo.Name, styles);
            }

            return styles[index];
        }

        static Texture2D createTexture(int width, int height, Color color)
        {
            var pixels = new Color[width * height];
            for (var i = 0; i < pixels.Length; ++i) 
                pixels[i] = color;

            var result = new Texture2D(width, height);
            result.SetPixels(pixels);
            result.Apply();
            return result;
        }

        static IComponentDrawer getComponentDrawer(Type type)
        {
            foreach (var drawer in _componentDrawers)
                if (drawer.HandlesType(type))
                    return drawer;

            return null;
        }

        static ITypeDrawer getTypeDrawer(Type type)
        {
            foreach (var drawer in _typeDrawers)
                if (drawer.HandlesType(type))
                    return drawer;

            return null;
        }
    }
}
