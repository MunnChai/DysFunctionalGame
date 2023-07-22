using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterEnemy : Enemy
{
    [SerializeField] private float fireRate = 1;
    [SerializeField] private float defaultSpeed = 100;

    public GameObject laserPrefab;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        speed = defaultSpeed;
        try {
            SetDirection(player.transform.position);
        } catch (NullReferenceException e) {
            
        }  
        InvokeRepeating("ShootLaser", fireRate, 3);
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player playerScript = other.gameObject.GetComponent<Player>();
            PlayerHealth playerHealthScript = other.gameObject.GetComponent<PlayerHealth>();
            if (!playerScript.invulnerable)
                playerHealthScript.TakeDamage(1);
                // Play explosion animation
        }
    }

    // Spawns a LaserEnemy on current position
    private void ShootLaser() {
        Instantiate(laserPrefab, transform.position, Quaternion.identity);
    }

    
}
