using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject player;
    protected GameObject enemySpawnManager;
    protected Enemy enemy;
    protected float xMultiplier;
    protected float yMultiplier;
    protected float angle;
    [SerializeField] protected float speed;
    

    // Start is called before the first frame update
    protected void Start() {
        gameObject.tag = "Enemy";
        player = GameObject.Find("Player");
        enemySpawnManager = GameObject.Find("EnemySpawnManager");
        if (player == null) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    protected void Update() {
        Move();
        DeleteOutOfBounds();
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

    // Deletes game object when it travels too far off screen
    protected void DeleteOutOfBounds() {
        float x = transform.position.x;
        float y = transform.position.y;

        float distOffscreen = 30; // How far offscreen the enemy will go before being deleted

        if (x > Constants.rightBound + distOffscreen || x < Constants.leftBound - distOffscreen ||
            y > Constants.topBound + distOffscreen || y < Constants.bottomBound - distOffscreen) {
                Destroy(gameObject);
            }
    }
}


