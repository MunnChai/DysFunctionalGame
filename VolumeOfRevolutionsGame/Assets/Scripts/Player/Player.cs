using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;

    [SerializeField] private float defaultSpeed;
    [SerializeField] private float defaultDashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldownTime;

    public bool invulnerable;
    private bool dashing;
    private bool dashOnCooldown;

    private PlayerParticles playerParticles;
    private Animator animator;
    private PlayerHealth playerHealthScript;

    // Start is called before the first frame update
    private void Start() {
        gameObject.tag = "Player";
        playerParticles = particleSystem.GetComponent<PlayerParticles>();
        animator = gameObject.GetComponent<Animator>();
        playerHealthScript = gameObject.GetComponent<PlayerHealth>();
        invulnerable = false;
    }

    // Update is called once per frame
    private void Update() {
        // Movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (!dashing) {
            Move(h, v, defaultSpeed);
            if (Input.GetKeyDown(KeyCode.LeftShift) && (h != 0 || v != 0) && !dashOnCooldown) {
                StartCoroutine(Dash(h, v));
                HandleDashAnimation(h, v);
                StartCoroutine(playerParticles.DashParticles(dashDuration));
            }
        }
        CheckOutOfBounds();
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

    private IEnumerator Dash(float h, float v) {
        float dashSpeed = defaultDashSpeed;
        float dashDeceleration = (defaultDashSpeed - defaultSpeed) / dashDuration;
        
        StartCoroutine(Invulnerability(dashDuration));
        dashing = true;

        for (float t = 0; t < dashDuration; t += Time.deltaTime) {
            Move(h, v, dashSpeed);
            dashSpeed -= dashDeceleration * Time.deltaTime;
            if (dashSpeed < defaultSpeed) {
                dashSpeed = defaultSpeed;
            }
            yield return null;
        }
        dashing = false;
        StartCoroutine(DashCooldown());
    }

    // Dealing with collisions
    private void OnTriggerEnter2D(Collider2D other) {

    }

    // Fixes player position when they go out of bounds
    private void CheckOutOfBounds() {
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
