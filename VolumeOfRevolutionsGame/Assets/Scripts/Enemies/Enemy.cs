using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject player;
    protected Enemy enemy;
    protected float xMultiplier;
    protected float yMultiplier;
    protected float angle;
    protected float speed;

    // Start is called before the first frame update
    protected void Start() {
        gameObject.tag = "Enemy";
        player = GameObject.Find("Player");
    }

    // Sets the angle that the gameObject will move towards
    protected void SetDirection(Vector3 target) {
        float xDist = player.transform.position.x - transform.position.x;
        float yDist = player.transform.position.y - transform.position.y;
        if (xDist > 0) {
            xMultiplier = 1;
        } else if (xDist < 0) {
            xMultiplier = -1;
        }
        if (yDist > 0) {
            yMultiplier = 1;
        } else if (yDist < 0) {
            yMultiplier = -1;
        }
        angle = Mathf.Atan(yDist / xDist);
    }

    // Move in the angle which was set
    protected void Move() {
        float ySpeed = speed * Mathf.Sin(angle) * xMultiplier * Time.deltaTime;
        float xSpeed = speed * Mathf.Cos(angle) * xMultiplier * Time.deltaTime;
        transform.position += new Vector3(xSpeed, ySpeed, 0);
    }

    protected abstract void OnTriggerEnter(Collider other);
}


