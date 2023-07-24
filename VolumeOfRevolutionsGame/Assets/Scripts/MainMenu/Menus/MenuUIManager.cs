using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelectMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainMenu() {
        StartCoroutine(WaitThenShowMenu(mainMenu, levelSelectMenu, 0.1f));
    }

    public void ShowLevelMenu() {
        StartCoroutine(WaitThenShowMenu(levelSelectMenu, mainMenu, 0.1f));
    }

    private IEnumerator WaitThenShowMenu(GameObject shownMenu, GameObject hiddenMenu, float duration) {
        yield return new WaitForSeconds(duration);
        shownMenu.SetActive(true);
        hiddenMenu.SetActive(false);
    }
}
