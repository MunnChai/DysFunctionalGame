using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Enemy
{
    [SerializeField] private ParticleSystem particleSystem;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        try {
            SetDirection(player.transform.position);
        } catch (NullReferenceException e) {
            
        }
        var newShape = particleSystem.shape;
        var newAngle = angle * 180 / Mathf.PI;
        newShape.rotation = new Vector3(0, newAngle, 0);
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
                playerHealthScript.TakeDamage(1);
                // Play explosion animation
        }
    }
}

