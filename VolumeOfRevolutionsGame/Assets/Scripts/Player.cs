using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float defaultSpeed;
    public float defaultDashSpeed;
    public float dashDecel;

    private bool isDashing;
    private float dashSpeed;
    private float hDashDirection;
    private float vDashDirection;

    // Start is called before the first frame update
    void Start() {
        defaultSpeed = 5;
        defaultDashSpeed = 30;
        dashDecel = 0.01f;
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update() {
        // Movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!isDashing) {
            Move(h, v, defaultSpeed);
            if (Input.GetKeyDown(KeyCode.LeftShift) && (h != 0 || v != 0)) {
                isDashing = true;
                hDashDirection = h;
                vDashDirection = v;
                dashSpeed = defaultDashSpeed;
            }
        }
        if (isDashing) {
            Move(hDashDirection, vDashDirection, dashSpeed);
            dashSpeed -= dashDecel * dashSpeed;
            if (dashSpeed <= defaultSpeed) {
                isDashing = false;
            }
        }
        FixPos();
    }

    // Move the player in a given direction and speed
    private void Move(float h, float v, float speed) {
        float diagonal = 1f;

        if (h != 0 && v != 0) {
            diagonal = 1 / Mathf.Sqrt(2);
        }

        float x = h * speed * diagonal * Time.deltaTime;
        float y = v * speed * diagonal * Time.deltaTime;

        transform.position += new Vector3(x, y, 0);
    }

    // Dealing with collisions
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Enemy") {
            // TODO 
        }
    }

    // Fixes player position when they go out of bounds
    private void FixPos() {
        Vector3 pos = transform.position;
        float x;
        float y;
        float right = Constants.rightBound - transform.localScale.x / 2;
        float left = Constants.leftBound + transform.localScale.x / 2;
        float top = Constants.topBound - transform.localScale.y / 2;
        float bottom = Constants.bottomBound + transform.localScale.y / 2;

        if (pos.x > right) {
            x = right;
        } else if (pos.x < left) {
            x = left;
        } else {
            x = pos.x;
        }
        if (pos.y > top) {
            y = top;
        } else if (pos.y < bottom) {
            y = bottom;
        } else {
            y = pos.y;
        }

        pos.x = x;
        pos.y = y;

        transform.position = pos;
    }
}
