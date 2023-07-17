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
        defeatMenu.ShowMenu();
        StartCoroutine(background.FadeTransparency(90, 2));
    }

    // Hides given menu
    public void HideDefeatMenu() {
        defeatMenu.HideMenu();
    }
}
