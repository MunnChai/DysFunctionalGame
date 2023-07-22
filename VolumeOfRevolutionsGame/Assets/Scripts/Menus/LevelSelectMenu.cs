using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelectMenu : Menu
{
    [SerializeField] private GameObject levelTitleObject1;
    [SerializeField] private GameObject levelTitleObject2;
    private TMP_Text levelTitle1;
    private TMP_Text levelTitle2;
    private TMP_Text activeTitle;
    private TMP_Text inactiveTitle;
    private bool onCooldown = false;
    private float selectCooldown = 0.2f;

    private ArrayList levels = new ArrayList();
    private Level currentLevel;
    private int currentIndex;

    private int numLevels;

    // Start is called before the first frame update
    void Start()
    {
        levelTitle1 = levelTitleObject1.GetComponent<TMP_Text>();
        levelTitle2 = levelTitleObject2.GetComponent<TMP_Text>();
        activeTitle = levelTitle1;
        inactiveTitle = levelTitle2;
        var tempColor = inactiveTitle.color;
        inactiveTitle.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);

        levels.Add(new Level("Sin(x)"));
        levels.Add(new Level("Cos(x)"));
        levels.Add(new Level("Tan(x)"));
        levels.Add(new Level("Sin(x)+1"));
        numLevels = 4;

        SetLevel((Level) levels[0]);
        currentIndex = 0;
    }

    // Loads the level scene with given parameters as settings for the level
    public void PlayLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Sets current level to next level in arraylist of levels
    public void NextLevel() {
        if (!onCooldown) {
            currentIndex = (currentIndex + 1) % numLevels;
            var level = (Level) levels[currentIndex];
            SetLevel(level);
        }
    }

    // Sets current level to previous level in arraylist of levels
    public void PrevLevel() {
        if (!onCooldown) {
            currentIndex = (currentIndex - 1) % numLevels;
            if (currentIndex < 0) {
                currentIndex = numLevels - 1;
            }
            var level = (Level) levels[currentIndex];
            SetLevel(level);
        }
    }

    // Sets active level to given level
    private void SetLevel(Level level) {
        (activeTitle, inactiveTitle) = (inactiveTitle, activeTitle);
        currentLevel = level;
        activeTitle.text = level.GetName().ToUpper();
        StartCoroutine(FadeTextTransparency(activeTitle, 100, selectCooldown));
        StartCoroutine(FadeTextTransparency(inactiveTitle, 0, selectCooldown));
        StartCoroutine(StartCooldown(selectCooldown));
    }

    private IEnumerator StartCooldown(float seconds) {
        onCooldown = true;
        yield return new WaitForSeconds(seconds);
        onCooldown = false;
    }

    private IEnumerator FadeTextTransparency(TMP_Text text, float percentage, float duration) {
        float r = text.color.r;
        float g = text.color.g;
        float b = text.color.b;
        float transparency = (percentage / 100);

        Color startColor = text.color;
        Color newColor = new Color(r, g, b, transparency);

        for (float t = 0; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            text.color = Color.Lerp(startColor, newColor, normalizedTime);
            yield return null;
        }
        text.color = newColor; 
    }
}
