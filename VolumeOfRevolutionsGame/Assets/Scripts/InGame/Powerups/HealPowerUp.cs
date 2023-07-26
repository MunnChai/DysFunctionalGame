using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : PowerUp
{
    private new void OnTriggerEnter2D(Collider2D other) {
        base.OnTriggerEnter2D(other);
        if (other.tag == "Player" && !playerHealth.gameOver) {
            playerHealth.Heal(2);
            Destroy(gameObject);
        }
    }
}