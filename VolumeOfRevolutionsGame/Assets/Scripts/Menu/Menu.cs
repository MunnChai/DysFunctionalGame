using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Shows Menu
    public void ShowMenu() {
        gameObject.SetActive(true);
    }

    // Hides Menu
    public void HideMenu() {
        gameObject.SetActive(false);
    }
}
