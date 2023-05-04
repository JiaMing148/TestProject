using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using System.Reflection;

public static class StuffUtils 
{
    // 自动Touch
    public static void Touches(){
        var types = GetSubClassesInAllAssemblies<IAutoTouch>();
        foreach(var t in types){
            var stuffType = t.BaseType;
            var stuffProp = stuffType.GetProperty("Stuff");
            var stuff = stuffProp.GetValue(null);
        }
        if(Application.isEditor){
            var typesEditor = GetSubClassesInAllAssemblies<IAutoTouchEditor>();
            foreach(var t in typesEditor){
                var stuffType = t.BaseType;
                var stuffProp = stuffType.GetProperty("Stuff");
                var stuff = stuffProp.GetValue(null);
            }
        }
    }

    public static List<Type> GetSubClasses<T>(Assembly assembly)
    {
        var subTypeQuery = from t in assembly.GetTypes()
                           where (typeof(T).IsAssignableFrom(t) && typeof(T) != t)
                           select t;
        return subTypeQuery.ToList();
    }

    public static List<Type> GetSubClassesInAllAssemblies<T>()
    {
        var assemblyList = AppDomain.CurrentDomain.GetAssemblies();
        var ret = new List<Type>();
        foreach(var assembly in assemblyList)
        {
            var list = GetSubClasses<T>(assembly);
            ret.AddRange(list);
        }
        return ret;
    }
    
}


public interface IAutoTouch 
{
    
}

public interface IAutoTouchEditor
{
    
}
