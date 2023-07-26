using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupParticles : MonoBehaviour
{
    private PowerUp powerup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fall());
    }

    private IEnumerator Fall() {
        yield return null;
        if (powerup == null) {
            StartCoroutine(StopEmissions());
        } else {
            transform.position += new Vector3(0, -powerup.fallSpeed * Time.deltaTime, 0);
            StartCoroutine(Fall());
        }
    }

    private IEnumerator StopEmissions() {
        var particleSystem = gameObject.GetComponent<ParticleSystem>();
        var emission = particleSystem.emission;
        emission.rateOverTime = 0;

        yield return new WaitForSeconds(particleSystem.main.startLifetime.constantMax);
        Destroy(gameObject);
    }

    public void SetPowerup(PowerUp powerup) {
        this.powerup = powerup;
    } 
}
