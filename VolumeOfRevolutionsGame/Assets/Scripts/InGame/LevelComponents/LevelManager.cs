using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject progressBarObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float levelDuration;
    
    private Slider progressBar;
    private PlayerHealth playerHealth;
    private float levelSpeed;


    // Start is called before the first frame update
    void Start()
    {
        progressBar = progressBarObject.GetComponent<Slider>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        levelSpeed = 10 / levelDuration * Time.deltaTime;
        StartCoroutine(AddProgress(levelSpeed));
    }

    private IEnumerator AddProgress(float amount) {
        yield return null;
        if (!playerHealth.gameOver) {
            progressBar.value += amount;
            StartCoroutine(AddProgress(levelSpeed));
        }
    }
}
