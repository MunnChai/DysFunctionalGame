using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private float dashLifetime;
    [SerializeField] private float dashSize;
    [SerializeField] private Color dashColor;

    [SerializeField] private float hitLifetime;
    [SerializeField] private float hitSize;
    [SerializeField] private Color hitColor;

    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule main;
    private ParticleSystem.EmissionModule emission;

    private ParticleSystem.MinMaxCurve originalLifetime;
    private ParticleSystem.MinMaxCurve originalSize;
    private ParticleSystem.MinMaxGradient originalColor;
    private ParticleSystem.MinMaxCurve originalRateOverTime;
    private ParticleSystem.MinMaxCurve originalRateOverDistance;

    void Start() {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        main = particleSystem.main;
        emission = particleSystem.emission;
        originalLifetime = main.startLifetime;
        originalSize = main.startSize;
        originalColor = main.startColor;
        originalRateOverTime = emission.rateOverTime;
        originalRateOverDistance = emission.rateOverDistance;
    }

    public void ChangeParticles(float lifetime, float size, Color color) {
        main.startLifetime = lifetime;
        main.startSize = size;
        main.startColor = color;
    }

    public void ReturnOriginalParticles() {
        main.startLifetime = originalLifetime;
        main.startSize = originalSize;
        main.startColor = originalColor;
        emission.rateOverTime = originalRateOverTime;
        emission.rateOverDistance = originalRateOverDistance;
    }

    public IEnumerator DashParticles(float dashDuration) {
        ChangeParticles(dashLifetime, dashSize, dashColor);
        yield return new WaitForSeconds(dashDuration);
        ReturnOriginalParticles();
    }

    public IEnumerator HitParticles() {
        ChangeParticles(hitLifetime, hitSize, hitColor);
        emission.rateOverTime = 20f;
        emission.rateOverDistance = 0f;
        yield return new WaitForSeconds(0.3f);
        ReturnOriginalParticles();
    }

    public IEnumerator DeathParticles() {
        yield return new WaitForSeconds(0.7f);
        ChangeParticles(hitLifetime, hitSize, hitColor);
        emission.rateOverTime = 20f;
        emission.rateOverDistance = 0f;
        yield return new WaitForSeconds(2f);
    }
}
