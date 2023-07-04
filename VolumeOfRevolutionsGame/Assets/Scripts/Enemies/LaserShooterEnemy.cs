using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterEnemy : Enemy
{
    public GameObject laserPrefab;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        speed = 1;
        SetDirection(player.transform.position);
        InvokeRepeating("ShootLaser", 1, 3);
    }

    // Update is called once per frame
    protected new void Update()
    {
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

    // Spawns a LaserEnemy on current position
    private void ShootLaser() {
        Instantiate(laserPrefab, transform.position, Quaternion.identity);
    }

    
}
