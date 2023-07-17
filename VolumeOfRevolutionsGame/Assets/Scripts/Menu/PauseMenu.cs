using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : Menu
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Loads Menu scene
    public void ReturnToMenu() {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % 2);
    }
}
