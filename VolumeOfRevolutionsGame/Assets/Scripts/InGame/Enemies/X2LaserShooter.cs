using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X2LaserShooter : LaserShooterEnemy
{
    // Spawns a LaserEnemy on current position
    private new void ShootLaser() {
        var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.transform.parent = enemySpawnManager.transform;
        enemySpawnManager.GetComponent<EnemySpawnManager>().enemies.Add(laser);
    }
}
