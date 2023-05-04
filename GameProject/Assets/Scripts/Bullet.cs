using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycledGameObject
{
    [SerializeField]
    protected float bulletSpeed;//子弹速度

    [SerializeField]
    protected float activeTime = 10f;

    protected float timer;

    private Rigidbody m_rb;

    public Rigidbody M_Rb
    {
        get
        {
            if (m_rb == null)
            {
                m_rb = GetComponent<Rigidbody>();
            }

            return m_rb;
        }
    }

    public void Bui(Vector3 dir, float force)
    {
        if (M_Rb.IsSleeping())
        {
            M_Rb.WakeUp();
        }

        M_Rb.AddForce(dir * force);
    }

    public override void OnRecycle()
    {
        timer = 0f;
        M_Rb.Sleep();
    }

    protected void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= activeTime)
        {
            Recycle();
        }
    }
}
