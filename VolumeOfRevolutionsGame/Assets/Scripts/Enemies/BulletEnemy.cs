using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Enemy
{
    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        speed = 7.5f;
        try {
            SetDirection(player.transform.position);
        } catch (NullReferenceException e) {
            
        }  
    }

    // Update is called once per frame
    protected new void Update() {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            Player playerScript = other.gameObject.GetComponent<Player>();
            PlayerHealth playerHealthScript = other.gameObject.GetComponent<PlayerHealth>();
            if (!playerScript.invulnerable)
                playerHealthScript.health -= 1;
                // Play explosion animation
        }
    }
}

