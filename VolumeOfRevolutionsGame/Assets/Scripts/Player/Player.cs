using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float defaultDashSpeed;
    [SerializeField] private float dashDecel;
    [SerializeField] private float dashCooldownTime;

    public bool invulnerable;

    private bool isDashing;
    private bool dashOnCooldown;
    private float dashSpeed;
    private float hDashDirection;
    private float vDashDirection;

    private Animator animator;
    private PlayerHealth playerHealthScript;

    // Start is called before the first frame update
    private void Start() {
        gameObject.tag = "Player";
        animator = gameObject.GetComponent<Animator>();
        playerHealthScript = gameObject.GetComponent<PlayerHealth>();
        invulnerable = false;
    }

    // Update is called once per frame
    private void Update() {
        // Movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!isDashing) {
            Move(h, v, defaultSpeed);
            if (Input.GetKeyDown(KeyCode.LeftShift) && (h != 0 || v != 0) && !dashOnCooldown) {
                isDashing = true;
                invulnerable = true;
                hDashDirection = h;
                vDashDirection = v;
                dashSpeed = defaultDashSpeed;
                HandleDashAnimation(h, v);
            }
        }
        if (isDashing) {
            Move(hDashDirection, vDashDirection, dashSpeed);
            dashSpeed -= dashDecel * dashSpeed;
            if (dashSpeed <= defaultSpeed) {
                isDashing = false;
                invulnerable = false;
                StartCoroutine(DashCooldown());
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

    // Determines which dash animation to play depending on which direction player is dashing
    private void HandleDashAnimation(float h, float v) {
        if (h == 0) {
            animator.SetTrigger("DashVertical");
        } else if (v == 0) {
            animator.SetTrigger("DashHorizontal");
        } else if (h * v > 0) {
            animator.SetTrigger("DashNorthEast");
        } else {
            animator.SetTrigger("DashSouthEast");
        }
    }

    // Leaves dash on cooldown for a set amount of seconds
    private IEnumerator DashCooldown() {
        dashOnCooldown = true;
        yield return new WaitForSeconds(dashCooldownTime);
        dashOnCooldown = false;
    }

    // Sets the player to invulnerable for given seconds
    public IEnumerator Invulnerability(float seconds) {
        invulnerable = true;
        yield return new WaitForSeconds(seconds);
        invulnerable = false;
    }
}
