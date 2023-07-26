using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject defeatMenuObject;
    [SerializeField] GameObject pauseMenuObject;
    [SerializeField] GameObject victoryMenuObject;
    [SerializeField] GameObject backgroundObject;
    [SerializeField] GameObject foregroundObject;
    [SerializeField] GameObject audioSourceObject;
    [SerializeField] AudioClip uiInteractSFX;

    private PauseMenu defeatMenu;
    private PauseMenu pauseMenu;
    private PauseMenu victoryMenu;
    private BackGround background;
    private ForeGround foreground;
    private AudioSource audioSource;

    private bool gameOver = false;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        defeatMenu = defeatMenuObject.GetComponent<PauseMenu>();
        pauseMenu = pauseMenuObject.GetComponent<PauseMenu>();
        victoryMenu = victoryMenuObject.GetComponent<PauseMenu>();
        background = backgroundObject.GetComponent<BackGround>();
        foreground = foregroundObject.GetComponent<ForeGround>();
        audioSource = audioSourceObject.GetComponent<AudioSource>();

        StartCoroutine(foreground.FadeOut(0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            if (!paused && !gameOver) {
                Pause();
            } else if (!gameOver) {
                Unpause();
            }
        }
    }

    public void RestartLevel() {
        foreground.FadeIn(0.5f);
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1f;
    }

    public void Pause() {
        PlayInteractSFX();
        paused = true;
        Time.timeScale = 0f;
        background.SetTransparency(90);
        ShowPauseMenu();
    }

    public void Unpause() {
        PlayInteractSFX();
        paused = false;
        Time.timeScale = 1f;
        background.SetTransparency(0);
        HidePauseMenu();
    }

    public void ShowPauseMenu() {
        pauseMenu.ShowMenu();
    }

    public void HidePauseMenu() {
        pauseMenu.HideMenu();
    }

    public void ShowDefeatMenu() {
        gameOver = true;
        defeatMenu.ShowMenu();
        StartCoroutine(background.FadeTransparency(90, 2));
    }

    public void HideDefeatMenu() {
        defeatMenu.HideMenu();
    }

    public void ShowVictoryMenu() {
        gameOver = true;
        victoryMenu.ShowMenu();
        StartCoroutine(background.FadeTransparency(100, 2));
    }

    public void HideVictoryMenu() {
        victoryMenu.HideMenu();
    }

    public void PlayInteractSFX() {
        audioSource.PlayOneShot(uiInteractSFX, 0.5f);
    }  
}
