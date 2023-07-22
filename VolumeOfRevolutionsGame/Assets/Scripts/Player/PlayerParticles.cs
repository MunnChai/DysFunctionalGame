using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private float dashLifetime;
    [SerializeField] private float dashSize;
    [SerializeField] private Color dashColor;

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

    public IEnumerator DashParticles(float dashDuration) {
            main.startLifetime = dashLifetime;
            main.startSize = dashSize;
            main.startColor = dashColor;
            yield return new WaitForSeconds(dashDuration);
            main.startLifetime = originalLifetime;
            main.startSize = originalSize;
            main.startColor = originalColor;
    }
}
