using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEnemy : Enemy
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip laserCharge, laserShoot;

    private GameObject particleManager;

    private static float chargeDuration = 1f;
    private static float fireDuration = 0.8f;
    private Animator animator;

    private bool activated;

    // Start is called before the first frame update
    protected new void Start() {
        base.Start();
        particleManager = GameObject.Find("ParticleManager");
        animator = gameObject.GetComponent<Animator>();
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        try {
            SetDirection(player.transform.position);
            RotateToPlayer();
            StartCoroutine(ChargeLaser());
        } catch (NullReferenceException e) {
            
        } 
        var laserParticles = Instantiate(particleSystem, transform.position, Quaternion.identity);
        laserParticles.GetComponent<LaserParticles>().RotateToPlayer(angle * 180 / Mathf.PI);
        laserParticles.transform.parent = particleManager.transform;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player" && activated) {
            PlayerHealth playerHealthScript = other.gameObject.GetComponent<PlayerHealth>();
            playerHealthScript.TakeDamage(1);
        }
    }

    // Waits for some seconds, then activates for some seconds before being deleted
    private IEnumerator ChargeLaser() {
        audioSource.PlayOneShot(laserCharge, 0.1f);
        yield return new WaitForSeconds(chargeDuration);
        audioSource.PlayOneShot(laserShoot, 0.1f);
        activated = true;
        yield return new WaitForSeconds(fireDuration);
        activated = false;
        yield return new WaitForSeconds(1 - fireDuration);
        Destroy(gameObject);
    }

    // Set rotation towards player
    private void RotateToPlayer() {
        transform.Rotate(new Vector3(0, 0, angle * 180 / Mathf.PI), Space.World);
    }
}