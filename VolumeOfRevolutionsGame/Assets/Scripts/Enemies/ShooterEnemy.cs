using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : Enemy
{
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        speed = 2.5f;
        SetDirection(player.transform.position);
        InvokeRepeating("ShootBullet", 1, 1);
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    protected override void OnTriggerEnter(Collider other) {
        // pass
    }

    private void ShootBullet() {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
