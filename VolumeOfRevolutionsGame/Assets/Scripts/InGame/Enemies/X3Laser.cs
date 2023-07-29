using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X3Laser : LaserEnemy
{
    [SerializeField] private float rotateSpeed;

    // Start is called before the first frame update
    private new void Start()
    {
        gameObject.tag = "Enemy";
        player = GameObject.Find("Player");
        enemySpawnManager = GameObject.Find("EnemySpawnManager");
        if (player == null) {
            Destroy(gameObject);
        }
        particleManager = GameObject.Find("ParticleManager");
        animator = gameObject.GetComponent<Animator>();
        audioSource = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        try {
            SetDirection(player.transform.position);
            StartCoroutine(ChargeLaser());
        } catch (NullReferenceException e) {
            
        } 
        var laserParticles = Instantiate(particleSystem, transform.position, Quaternion.identity);
        laserParticles.transform.parent = gameObject.transform;
        laserParticles.transform.rotation = new Quaternion();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }

    // Waits for some seconds, then activates for some seconds before being deleted
    private new IEnumerator ChargeLaser() {
        audioSource.PlayOneShot(laserCharge, 0.1f);
        yield return new WaitForSeconds(chargeDuration);
        audioSource.PlayOneShot(laserShoot, 0.1f);
        animator.SetTrigger("LaserBlast");
        activated = true;
    }
}
