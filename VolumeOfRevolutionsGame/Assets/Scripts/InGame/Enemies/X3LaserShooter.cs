using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X3LaserShooter : LaserShooterEnemy
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        speed = defaultSpeed;
        spinDirection = UnityEngine.Random.Range(-10, 10);
        
        try {
            SetDirection(player.transform.position);
        } catch (NullReferenceException e) {
            
        }  
        ShootLaser();
    }

    // Spawns a LaserEnemy on current position
    private new void ShootLaser() {
        var laser1 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        var laser2 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser2.transform.Rotate(new Vector3(0, 0, 180));
        laser1.transform.parent = gameObject.transform;
        laser2.transform.parent = gameObject.transform;
        enemySpawnManager.GetComponent<EnemySpawnManager>().enemies.Add(laser1);
        enemySpawnManager.GetComponent<EnemySpawnManager>().enemies.Add(laser2);
    }
}
