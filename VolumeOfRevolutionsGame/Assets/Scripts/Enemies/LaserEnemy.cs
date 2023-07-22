using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Enemy
{
    private static float chargeDuration = 1f;
    private static float fireDuration = 1f;
    private Animator animator;

    private bool activated;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        animator = gameObject.GetComponent<Animator>();
        try {
            SetDirection(player.transform.position);
            RotateToPlayer();
            StartCoroutine(ChargeLaser());
        } catch (NullReferenceException e) {
            
        } 
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (activated && other.tag == "Player") {
            Player playerScript = other.gameObject.GetComponent<Player>();
            PlayerHealth playerHealthScript = other.gameObject.GetComponent<PlayerHealth>();
            if (!playerScript.invulnerable)
                playerHealthScript.TakeDamage(2);
        }
    }

    // Waits for some seconds, then activates for some seconds before being deleted
    private IEnumerator ChargeLaser() {
        yield return new WaitForSeconds(chargeDuration);
        activated = true;
        yield return new WaitForSeconds(fireDuration);
        activated = false;
        Destroy(gameObject);
    }

    // Set rotation towards player
    private void RotateToPlayer() {
        transform.Rotate(new Vector3(0, 0, angle * 180 / Mathf.PI), Space.World);
    }
}
