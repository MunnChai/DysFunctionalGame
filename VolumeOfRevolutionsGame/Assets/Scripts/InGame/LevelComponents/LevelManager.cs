using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject uiManagerObject;
    [SerializeField] private GameObject progressBarObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject instructions;
    [SerializeField] public float levelDuration;

    [SerializeField] private GameObject enemySpawnObject;
    [SerializeField] private GameObject scoreManagerObject;
    [SerializeField] private GameObject powerupSpawnObject;
    [SerializeField] private GameObject functionManagerObject;

    private bool gameStarted;
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
        gameStarted = false;
        musicManager.PlaySong(0);
        musicIntensity = 0;
        musicManager.SetIntensity(0, 1f, 1f);
    }

    void Update() {
        if (Input.anyKey && !gameStarted) {
            StartCoroutine(BeginLevel());
        }
    }

    private IEnumerator BeginLevel() {
        gameStarted = true;
        instructions.SetActive(false);
        yield return new WaitForSeconds(3);
        StartCoroutine(AddProgress(levelSpeed));
        StartCoroutine(powerupSpawnObject.GetComponent<PowerUpManager>().SpawnPowerup());
        StartCoroutine(scoreManagerObject.GetComponent<ScoreManager>().PassiveScoreGain());
        StartCoroutine(enemySpawnObject.GetComponent<EnemySpawnManager>().SpawnEnemy(5.5f));
        StartCoroutine(functionManagerObject.GetComponent<MathFunction>().DrawCubes());
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
        if (progressBar.value >= progressBar.maxValue / 2 && (musicIntensity == 0)) {
            musicManager.SetIntensity(2, 1f, 1f);
            musicIntensity = 2;
        }
    }
}
