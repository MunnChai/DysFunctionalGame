using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    [SerializeField] private GameObject foregroundObject;

    private ForeGround foreground;

    // Start is called before the first frame update
    void Start()
    {
        foreground = foregroundObject.GetComponent<ForeGround>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Loads Menu scene
    public void ReturnToMenu() {
        foreground.FadeIn(0.5f);
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
    }
}
