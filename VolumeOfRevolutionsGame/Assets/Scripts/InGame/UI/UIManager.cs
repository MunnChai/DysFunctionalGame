using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject defeatMenuObject;
    [SerializeField] GameObject pauseMenuObject;
    [SerializeField] GameObject backgroundObject;

    private PauseMenu defeatMenu;
    private PauseMenu pauseMenu;
    private BackGround background;

    private bool gameOver = false;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        defeatMenu = defeatMenuObject.GetComponent<PauseMenu>();
        pauseMenu = pauseMenuObject.GetComponent<PauseMenu>();
        background = backgroundObject.GetComponent<BackGround>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape")) {
            if (!paused && !gameOver) {
                paused = true;
                Time.timeScale = 0f;
                ShowPauseMenu();
            } else {
                paused = false;
                Time.timeScale = 1f;
                HidePauseMenu();
            }
        }
    }

    // Shows given menu
    public void ShowPauseMenu() {
        pauseMenu.ShowMenu();
    }

    // Hides given menu, sets 
    public void HidePauseMenu() {
        pauseMenu.HideMenu();
    }

    // Shows given menu, fades in UI background
    public void ShowDefeatMenu() {
        gameOver = true;
        defeatMenu.ShowMenu();
        StartCoroutine(background.FadeTransparency(90, 2));
    }

    // Hides given menu
    public void HideDefeatMenu() {
        defeatMenu.HideMenu();
    }
}
