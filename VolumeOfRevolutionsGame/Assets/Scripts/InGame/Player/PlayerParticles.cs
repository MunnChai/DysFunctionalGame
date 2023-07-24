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

    private ParticleSystem.MainModule main;

    private ParticleSystem.MinMaxCurve originalLifetime;
    private ParticleSystem.MinMaxCurve originalSize;
    private ParticleSystem.MinMaxGradient originalColor;

    void Start() {
        main = gameObject.GetComponent<ParticleSystem>().main;
        originalLifetime = main.startLifetime;
        originalSize = main.startSize;
        originalColor = main.startColor;
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
    }

    public IEnumerator DashParticles(float dashDuration) {
        ChangeParticles(dashLifetime, dashSize, dashColor);
        yield return new WaitForSeconds(dashDuration);
        ReturnOriginalParticles();
    }

    public IEnumerator HitParticles() {
        ChangeParticles(hitLifetime, hitSize, hitColor);
        yield return new WaitForSeconds(0.3f);
        ReturnOriginalParticles();
    }
}
