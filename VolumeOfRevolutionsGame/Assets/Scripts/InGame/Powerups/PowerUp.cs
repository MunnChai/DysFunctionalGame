using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] protected GameObject playerObject;
    [SerializeField] public float fallSpeed;
    [SerializeField] private AudioClip collectSFX;

    protected Player player;
    protected PlayerHealth playerHealth;
    protected AudioManager audioManager;

    // Start is called before the first frame update
    protected void Start()
    {
        playerObject = GameObject.Find("Player");
        player = playerObject.GetComponent<Player>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
        if (transform.position.y <= Constants.bottomBound - transform.localScale.y / 2) {
            Destroy(gameObject);
        }
    }

    protected void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            audioManager.PlaySoundEffect(collectSFX);
            ScoreManager.AddScore(25000);
        }
    }
}
