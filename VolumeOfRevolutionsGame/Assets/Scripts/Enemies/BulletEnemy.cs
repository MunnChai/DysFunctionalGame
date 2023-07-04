using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Enemy
{
    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        speed = 7.5f;
        SetDirection(player.transform.position);
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
}

