using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAvatar : MonoBehaviour
{
    public bool CursorVisible
    {
        get { return Cursor.visible; }
        set
        {
            if (Cursor.visible == value)
            {
                return;
            }

            Cursor.visible = value;
        }
    }

    private Rigidbody rb;

    private Rigidbody Rb
    {
        get
        {
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
            }
            return rb;
        }
    }

    [SerializeField]
    private Transform body;

    [SerializeField]
    private ShootBullet gun;

    [SerializeField]
    private float moveSpeed = 1f;

    private void Update()
    {
        CursorVisible = Input.GetKey(KeyCode.LeftAlt);
        if (Input.GetKeyDown(KeyCode.V))
        {
            gun.ChengeShootMode();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.Replace();
        }

        Shoot();
        //SyncGunForward();
        SyncForward();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var worldPos = transform.position;
        var dir = new Vector3(horizontal, 0f, vertical) * Time.fixedDeltaTime * moveSpeed;
        var selfAngles = Camera.main.transform.localEulerAngles;
        dir = Quaternion.Euler(selfAngles) * dir;
        dir.y = 0f;
        var nextPos = worldPos + dir;

        Rb.MovePosition(nextPos);
        body.transform.LookAt(nextPos);
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.OnShoot();
            return;
        }

        if (gun.mode == ShootType.RunningFire)
        {
            if (Input.GetMouseButton(0))
            {
                gun.OnShoot();
            }
        }
    }

    private void SyncForward()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        if (vertical > 0f)
        {
            var angles = body.transform.localEulerAngles;
            var cameraAngles = Camera.main.transform.localEulerAngles;
            body.transform.localEulerAngles = new Vector3(angles.x, cameraAngles.y, angles.z);
        }
    }

    private void SyncGunForward()
    {
        var cameraForward = Camera.main.transform.InverseTransformDirection(Vector3.forward);
        var worldPos = gun.transform.position + cameraForward;
        gun.transform.LookAt(worldPos);
        var pos = Camera.main.transform.position;
        Debug.DrawLine(pos, pos + cameraForward, Color.red);
    }
}
