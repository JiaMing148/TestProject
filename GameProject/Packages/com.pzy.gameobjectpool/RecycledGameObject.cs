using UnityEngine;
using System;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

[DisallowMultipleComponent]
#if ODIN_INSPECTOR
public class RecycledGameObject : SerializedMonoBehaviour
#else
public class RecycledGameObject : MonoBehaviour
#endif
{
#if ODIN_INSPECTOR
    [field: ReadOnly]
#endif
    public GameObject Prefab { get; set; }

    public GameObject GameObject => gameObject; 
    public Transform Transform => transform;
    
#if ODIN_INSPECTOR
    [ShowInInspector, ReadOnly]
#endif
    public bool IsVirgin { get; set; }
#if ODIN_INSPECTOR
    [ShowInInspector, ReadOnly]
#endif
    public bool IsInPool { get; set; }

	public event Action OnObjectReuse;

    public event Action<RecycledGameObject> OnObjectRecycle;

    public virtual void OnReuse()
    {
    }

    public virtual void OnRecycle()
    {
    }

    public void Recycle()
    {
        GameObjectPool.Stuff.Recycle(this);
    }

	public void TriggerOnObjectReuse()
	{
		OnObjectReuse?.Invoke();
	}

    public void TriggerOnObjectRecycle()
    {
        OnObjectRecycle?.Invoke(this);
        OnObjectRecycle = null;
    }
}
