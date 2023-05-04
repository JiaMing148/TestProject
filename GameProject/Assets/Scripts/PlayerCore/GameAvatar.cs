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

    [SerializeField]
    private Transform body;

    [SerializeField]
    private ShootBullet gun;

    private void Update()
    {
        CursorVisible = Input.GetKey(KeyCode.LeftAlt);
        Move();
        if (Input.GetKeyDown(KeyCode.V))
        {
            gun.ChengeShootMode();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.Replace();
        }

        Shoot();
        SyncGunForward();
    }

    private void Move()
    {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        if (horizontal == 0f && vertical == 0f)
        {
            return;
        }

        SyncForward();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.OnShoot();
            SyncForward();
            return;
        }

        if (gun.mode == ShootType.RunningFire)
        {
            if (Input.GetMouseButton(0))
            {
                gun.OnShoot();
                SyncForward();
            }
        }
    }

    private void SyncForward()
    {
        var angles = body.transform.localEulerAngles;
        var cameraAngles = Camera.main.transform.localEulerAngles;
        body.transform.localEulerAngles = new Vector3(angles.x, cameraAngles.y, angles.z);
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
