using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if ODIN_INSPECTOR 
public class SceneObject<T> : SerializedMonoBehaviour where T : SceneObject<T>
#else
public class SceneObject<T> : MonoBehaviour where T : SceneObject<T>
#endif

{
    private static T _stuff;

    public static T Stuff
    {
        get
        {
            if (_stuff == null)
            {
                _stuff = FindObjectOfType<T>(true);
                if (_stuff is null)
                {
                    throw new SceneObjectNotFoundException($"SceneObject: {(typeof(T).Name)} not found in current scene");
                }
            }

            return _stuff;
        }
    }
    
    public static bool Actived{
        get{
            return _stuff;
        }
    }
}
    
public class SceneObjectNotFoundException : Exception
{
    public SceneObjectNotFoundException(string message) : base(message)
    {
    }
}