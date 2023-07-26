using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject uiManagerObject;
    [SerializeField] private GameObject progressBarObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] public float levelDuration;

    public float progress;
    
    private Manager musicManager;
    private int musicIntensity;

    private UIManager uiManager;
    private Slider progressBar;
    private Player player;
    private PlayerHealth playerHealth;
    private float levelSpeed;

    // Start is called before the first frame update
    void Start()
    {
        musicManager = Manager.instance;
        uiManager = uiManagerObject.GetComponent<UIManager>();
        progressBar = progressBarObject.GetComponent<Slider>();
        player = playerObject.GetComponent<Player>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        levelSpeed = 100 / levelDuration;
        StartCoroutine(AddProgress(levelSpeed));
        musicManager.PlaySong(0);
        musicIntensity = 0;
        musicManager.SetIntensity(0, 1f, 1f);
    }

    private IEnumerator AddProgress(float amount) {
        yield return null;
        if (!playerHealth.gameOver) {
            progress += amount * Time.deltaTime;
            progressBar.value = progress;
            StartCoroutine(AddProgress(levelSpeed));
        }
        if (progressBar.value >= progressBar.maxValue) {
            playerHealth.gameOver = true;
            uiManager.ShowVictoryMenu();
        }
        if (progressBar.value >= progressBar.maxValue / 2 && (musicIntensity == 1)) {
            musicManager.SetIntensity(2, 1f, 1f);
            musicIntensity = 2;
        }
    }
}
