using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delete());
    }

    public void RotateToPlayer(float rotation) {
        transform.Rotate(new Vector3(0, 0, rotation), Space.World);
    }

    private IEnumerator Delete() {
        yield return new WaitForSeconds(1.5f);
        var particleSystem = gameObject.GetComponent<ParticleSystem>();
        var emission = particleSystem.emission;
        emission.rateOverTime = 0f;
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }

    
}
