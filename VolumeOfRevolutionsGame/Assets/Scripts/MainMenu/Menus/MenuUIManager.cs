using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelectMenu;
    [SerializeField] private GameObject foregroundObject;

    private ForeGround foreground;

    void Start() {
        foreground = foregroundObject.GetComponent<ForeGround>();
        StartCoroutine(foreground.FadeOut(0.5f));
    }

    public void PlayLevel() {
        foreground.FadeIn(0.5f);
        SceneManager.LoadSceneAsync(1);
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
