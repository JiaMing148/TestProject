using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public static class StuffRootHolder
{
    private static GameObject _stuffRoot;

    public static GameObject StuffRoot
    {
        get
        {
            if (_stuffRoot == null)
            {
                _stuffRoot = new GameObject();
                _stuffRoot.name = "StuffRoot";
                GameObject.DontDestroyOnLoad(_stuffRoot);
            }

            return _stuffRoot;
        }
    }

    private static Dictionary<string, Transform> stuffGroups = new Dictionary<string, Transform>();

    public static Transform GetParent<T>()
    {
        var attribute = typeof(T).GetCustomAttribute<StuffGroupAttribute>();
        string group;
        if (attribute is null)
        {
            group = "Default";
        }
        else
        {
            group = attribute.StuffGroup;
        }

        if (!stuffGroups.TryGetValue(group, out var parent))
        {
            parent =  new GameObject(group.ToString()).transform;
            parent.SetParent(StuffRoot.transform, false);
            stuffGroups[group] = parent;
        }

        return parent;
    }
}

public class StuffGroupAttribute : Attribute
{
    public string StuffGroup;

    public StuffGroupAttribute(string stuffGroup)
    {
        StuffGroup = stuffGroup;
    }
}