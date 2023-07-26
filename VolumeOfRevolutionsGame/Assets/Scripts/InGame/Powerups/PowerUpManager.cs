using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private float powerUpInterval;
    [SerializeField] private GameObject playerObject;

    public GameObject[] powerUps;
    public GameObject[] particles;

    private PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        StartCoroutine(SpawnPowerup());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnPowerup() {
        yield return new WaitForSeconds(powerUpInterval);
        int randInt = Random.Range((int) 0, (int) powerUps.Length);

        GameObject powerUp = Instantiate(powerUps[randInt], new Vector3(Random.Range(Constants.leftBound + 50, Constants.rightBound - 50), 
                                                              Constants.topBound + 20, 0), Quaternion.identity);
        GameObject particle = Instantiate(particles[randInt], powerUp.transform.position, Quaternion.identity);

        particle.GetComponent<PowerupParticles>().SetPowerup(powerUp.GetComponent<PowerUp>());

        powerUp.transform.parent = gameObject.transform;
        particle.transform.parent = gameObject.transform;

        if (!playerHealth.gameOver) {
            StartCoroutine(SpawnPowerup());
        }
    }
}
