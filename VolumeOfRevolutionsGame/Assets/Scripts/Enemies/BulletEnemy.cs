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
    void Update() {
        Move();
    }

    protected override void OnTriggerEnter(Collider other) {
        // pass
    }
}

