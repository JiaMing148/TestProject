using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform shootPoint;

    [SerializeField]
    private float shootSpeed = 5; //射击速度(子弹数/s)

    [SerializeField]
    private float shootForce = 10; //射击力度

    [SerializeField]
    private int capacity = 30; //弹夹容量

    [SerializeField]
    private int bullectNumber = 90;//子弹数量

    [ShowInInspector]
    private int curBulletNumber;//当前弹夹子弹

    private float lastShootTime;

    private int _shootModeIndex;

    private int ShootModeIndex
    {
        get { return _shootModeIndex; }
        set
        {
            if (value >= shootModes.Count)
            {
                value = 0;
            }
            _shootModeIndex = value;
            mode = shootModes[value];
            Debug.Log($"切换射击模式至{mode.ToString()}");
        }
    }

    [SerializeField]
    List<ShootType> shootModes = new List<ShootType>();

    [ShowInInspector, ReadOnly]
    public ShootType mode;   //射击类型

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ShootModeIndex = 0;
        curBulletNumber = capacity;
    }

    public void OnShoot()
    {
        if (curBulletNumber == 0)
        {
            Debug.Log("请换弹");
            return;
        }

        var interval = Time.time - lastShootTime;
        if (interval < 1f / shootSpeed) return;

        var bullet = GetBullet();
        var dir = transform.TransformDirection(Vector3.forward);

        bullet.Bui(dir, shootForce);
        curBulletNumber--;
        lastShootTime = Time.time;
    }

    public void Replace()
    {
        if (bullectNumber <= 0) return;
        bullectNumber += curBulletNumber;
        curBulletNumber = Mathf.Min(capacity, bullectNumber);
        bullectNumber -= curBulletNumber;

        Debug.Log($"换弹完成 当前弹夹:{curBulletNumber} 剩余弹夹:{bullectNumber}");
    }

    public void ChengeShootMode()
    {
        ShootModeIndex++;
    }

    private Bullet GetBullet()
    {
        var bullet = GameObjectPool.Stuff.Reuse<Bullet>(this.bullet);

        bullet.transform.position = shootPoint.position;
        bullet.transform.localRotation = Quaternion.identity;

        return bullet;
    }
}

public enum ShootType
{
    OneShoot,
    RunningFire
}
