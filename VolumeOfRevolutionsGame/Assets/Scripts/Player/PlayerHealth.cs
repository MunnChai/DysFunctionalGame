using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    public int health;

    private Player playerScript;
    private UIManager UIManager;

    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        playerScript = gameObject.GetComponent<Player>();
        UIManager = Canvas.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Subtract damage from current health and play taking damage animation
    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            GameOver();
        }
        // play damage animation
        StartCoroutine(playerScript.Invulnerability(1f));
    }

    

    // Plays player death animation, destroys player gameObject and shows defeat UI menu
    public void GameOver() {
        Destroy(gameObject);
        UIManager.ShowDefeatMenu();
    }
}

