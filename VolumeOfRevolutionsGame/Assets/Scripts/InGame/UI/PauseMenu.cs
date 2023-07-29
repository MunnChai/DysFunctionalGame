using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    [SerializeField] private GameObject foregroundObject;
    [SerializeField] private Button returnToMenuButton;

    private ForeGround foreground;

    // Start is called before the first frame update
    void Start()
    {
        foreground = foregroundObject.GetComponent<ForeGround>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            returnToMenuButton.onClick.Invoke();
        }
    }

    // Loads Menu scene
    public void ReturnToMenu() {
        foreground.FadeIn(0.5f);
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
    }
}
