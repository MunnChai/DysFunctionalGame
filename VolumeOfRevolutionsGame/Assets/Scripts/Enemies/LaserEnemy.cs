using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Enemy
{
    private bool activated;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        StartCoroutine(ChargeLaser());
        SetDirection(player.transform.position);
        RotateToPlayer();
    }

    // Update is called once per frame
    protected new void Update() {
        // pass
    }

    protected override void OnTriggerEnter(Collider other) {
        if (activated) {
            Destroy(other.gameObject);
        }
    }

    // Waits for some seconds, then activates for some seconds before being deleted
    private IEnumerator ChargeLaser() {
        yield return new WaitForSeconds(0.5f);
        activated = true;
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(0, 0, 1, 1);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    // Set rotation towards player
    private void RotateToPlayer() {
        transform.Rotate(new Vector3(0, 0, angle * 180 / Mathf.PI), Space.World);
    }
}
