using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterEnemy : Enemy
{
    

    [SerializeField] protected float fireRate;
    [SerializeField] protected float defaultSpeed = 100;

    public GameObject laserPrefab;

    protected float spinDirection;

    // Start is called before the first frame update
    private new void Start() {
        base.Start();
        speed = defaultSpeed;
        spinDirection = UnityEngine.Random.Range(-10, 10);
        
        try {
            SetDirection(player.transform.position);
        } catch (NullReferenceException e) {
            
        }  
        InvokeRepeating("ShootLaser", 1, fireRate);
    }

    // Update is called once per frame
    protected new void Update() {
        base.Update();
        transform.Rotate(new Vector3(0, 0, spinDirection * Time.deltaTime));
    }

    protected void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            PlayerHealth playerHealthScript = other.gameObject.GetComponent<PlayerHealth>();
            playerHealthScript.TakeDamage(1);
        }
    }

    // Spawns a LaserEnemy on current position
    private void ShootLaser() {
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.transform.parent = enemySpawnManager.transform;
        enemySpawnManager.GetComponent<EnemySpawnManager>().enemies.Add(laser);
    }
}
