using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject hurtFlash;

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
    private Animator cameraAnimator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        playerParticles = particleSystem.GetComponent<PlayerParticles>();
        playerScript = gameObject.GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
        UIManager = Canvas.GetComponent<UIManager>();
        playerSFX = gameObject.GetComponent<PlayerSFX>();
        cameraAnimator = camera.GetComponent<Animator>();
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Subtract damage from current health and play taking damage animation
    public void TakeDamage(int damage) {
        if (!playerScript.dashing && !playerScript.invulnerable && !gameOver) {
            health -= damage;
            playerSFX.HitSound();
            cameraAnimator.SetTrigger("Shake");
            StartCoroutine(HurtFlash(0.5f));
            if (health <= 0) {
                StartCoroutine(GameOver());
                playerSFX.DeathSound();
            } else {
                ScoreManager.AddHit(1);
                StartCoroutine(playerParticles.HitParticles());
                animator.SetTrigger("Hurt");
                StartCoroutine(playerScript.Invulnerability(1.5f));
            }
        }
        UpdateHealthBar();
    }

    public void Heal(int healAmount) {
        health += healAmount;
        if (health > 5) {
            health = 5;
        }
        UpdateHealthBar();
    }

    // Plays player death animation, destroys player gameObject and shows defeat UI menu
    private IEnumerator GameOver() {
        gameOver = true;
        StartCoroutine(playerScript.Invulnerability(5f));
        animator.SetTrigger("Death");
        StartCoroutine(playerParticles.DeathParticles());
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        UIManager.ShowDefeatMenu();
    }

    private void UpdateHealthBar() {
        for (int i = 0; i < maxHealth; i++) {
            if (i >= health) {
                hearts[i].GetComponent<Animator>().enabled = false;
                hearts[i].sprite = emptyHeart;
            } else {
                hearts[i].GetComponent<Animator>().enabled = true;
                hearts[i].sprite = fullHeart;
            }
        }
    }

    private IEnumerator HurtFlash(float duration) {
        var flash = Instantiate(hurtFlash, new Vector3(0, 0, 0), Quaternion.identity);
        
        var image = flash.GetComponent<SpriteRenderer>();
        float r = image.color.r;
        float g = image.color.g;
        float b = image.color.b;

        Color startColor = new Color(r, g, b, 0);
        Color newColor = new Color(r, g, b, 0.3f);

        image.color = startColor;
        for (float t = 0; t < duration / 3; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(startColor, newColor, normalizedTime);
            yield return null;
        }
        
        image.color = newColor;
        yield return new WaitForSeconds(duration / 3);

        for (float t = 0; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(newColor, startColor, normalizedTime);
            yield return null;
        }

        image.color = startColor;
        Destroy(flash);
    }
}

