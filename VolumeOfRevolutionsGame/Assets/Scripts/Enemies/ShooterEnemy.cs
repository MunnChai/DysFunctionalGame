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
    protected new void Update() {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player playerScript = other.gameObject.GetComponent<Player>();
            if (!playerScript.invulnerable)
                playerScript.health -= 1;
                // Play explosion animation
        }
    }

    private void ShootBullet() {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
