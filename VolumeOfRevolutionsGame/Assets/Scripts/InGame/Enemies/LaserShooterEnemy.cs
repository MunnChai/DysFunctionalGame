using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooterEnemy : Enemy
{
    

    [SerializeField] private float fireRate = 1;
    [SerializeField] private float defaultSpeed = 100;

    public GameObject laserPrefab;

    private float spinDirection;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        speed = defaultSpeed;
        spinDirection = UnityEngine.Random.Range(-10, 10);
        
        try {
            SetDirection(player.transform.position);
        } catch (NullReferenceException e) {
            
        }  
        InvokeRepeating("ShootLaser", fireRate, 3);
    }

    // Update is called once per frame
    protected new void Update() {
        base.Update();
        transform.Rotate(new Vector3(0, 0, spinDirection * Time.deltaTime));
    }

    private void OnTriggerStay2D(Collider2D other) {
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
