using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Menu
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Quits game
    public void QuitGame() {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public new void ShowMenu() {
        
    }

    public new void HideMenu() {

    }

    
}
