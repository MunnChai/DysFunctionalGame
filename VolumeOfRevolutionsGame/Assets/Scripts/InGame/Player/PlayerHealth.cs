using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private ParticleSystem particleSystem;

    [SerializeField] private int maxHealth = 5;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    public int health;
    public bool gameOver;

    private PlayerParticles playerParticles;
    private Player playerScript;
    private Animator animator;
    private UIManager UIManager;
    private PlayerSFX playerSFX;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerParticles = particleSystem.GetComponent<PlayerParticles>();
        playerScript = gameObject.GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
        UIManager = Canvas.GetComponent<UIManager>();
        playerSFX = gameObject.GetComponent<PlayerSFX>();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Subtract damage from current health and play taking damage animation
    public void TakeDamage(int damage) {
        if (!playerScript.dashing && !playerScript.invulnerable) {
            health -= damage;
            playerSFX.HitSound();
            if (health <= 0) {
                GameOver();
                playerSFX.DeathSound();
            } else {
                StartCoroutine(playerParticles.HitParticles());
                animator.SetTrigger("Hurt");
                StartCoroutine(playerScript.Invulnerability(1.5f));
            }
        }
        UpdateHealthBar();
    }

    // Plays player death animation, destroys player gameObject and shows defeat UI menu
    public void GameOver() {
        gameOver = true;
        Destroy(gameObject);
        UIManager.ShowDefeatMenu();
    }

    private void UpdateHealthBar() {
        for (int i = 0; i < maxHealth; i++) {
            if (i >= health) {
                hearts[i].GetComponent<Animator>().enabled = false;
                hearts[i].sprite = emptyHeart;
            } else {
                hearts[i].sprite = fullHeart;
            }
        }
    }
}

