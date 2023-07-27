using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectMenu : Menu
{
    [SerializeField] private GameObject levelTitleObject1;
    [SerializeField] private GameObject levelTitleObject2;
    [SerializeField] private GameObject menuBackgroundObject;
    [SerializeField] private Color[] levelColors;
    [SerializeField] private GameObject levelPreviewObject;

    [SerializeField] private GameObject gradeObject;
    [SerializeField] private GameObject scoreObject;
    [SerializeField] private GameObject hitsObject;
    [SerializeField] public Color[] gradeColors;
    private TMP_Text levelTitle1;
    private TMP_Text levelTitle2;
    private TMP_Text activeTitle;
    private TMP_Text inactiveTitle;
    private TMP_Text grade;
    private TMP_Text score;
    private TMP_Text hits;
    private MenuBackGround menuBackground;
    private LevelPreview levelPreview;
    private bool onCooldown = false;
    private float selectCooldown = 0.2f;
    private Coroutine previewCoroutine;

    [SerializeField] private ArrayList levels = new ArrayList();
    public static Level currentLevel;
    private int currentIndex;

    private int numLevels;

    // Start is called before the first frame update
    void Start()
    {
        levelPreview = levelPreviewObject.GetComponent<LevelPreview>();
        levelTitle1 = levelTitleObject1.GetComponent<TMP_Text>();
        levelTitle2 = levelTitleObject2.GetComponent<TMP_Text>();
        menuBackground = menuBackgroundObject.GetComponent<MenuBackGround>();
        grade = gradeObject.GetComponent<TMP_Text>();
        score = scoreObject.GetComponent<TMP_Text>();
        hits = hitsObject.GetComponent<TMP_Text>();
        activeTitle = levelTitle1;
        inactiveTitle = levelTitle2;
        var tempColor = inactiveTitle.color;
        inactiveTitle.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);

        levels.Add(new Level("Sin(x)", (Color) levelColors[0]));
        levels.Add(new Level("Cos(x)", (Color) levelColors[1]));
        levels.Add(new Level("Tan(x)", (Color) levelColors[2]));
        levels.Add(new Level("Sin(x)+Cos(2x+2)", (Color) levelColors[3]));
        numLevels = 4;

        SetLevel((Level) levels[0]);
        currentIndex = 0;
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
        if (previewCoroutine != null) {
            StopCoroutine(previewCoroutine);
        }
        (activeTitle, inactiveTitle) = (inactiveTitle, activeTitle);
        currentLevel = level;
        activeTitle.text = level.GetName().ToUpper();
        StartCoroutine(menuBackground.FadeToColor(level.GetColor(), selectCooldown));
        StartCoroutine(FadeTextTransparency(activeTitle, 100, selectCooldown));
        StartCoroutine(FadeTextTransparency(inactiveTitle, 0, selectCooldown));
        StartCoroutine(StartCooldown(selectCooldown));
        foreach(LevelHighscore levelHighscore in SaveData.highScores) {
            if (levelHighscore.levelName == currentLevel.GetName()) {
                grade.text = levelHighscore.grade;
                grade.color = GetGradeColor(grade.text);
                score.text = levelHighscore.score.ToString();
                hits.text = levelHighscore.hits.ToString();
            }
        }
        previewCoroutine = StartCoroutine(levelPreview.UpdatePreview(currentLevel));
    }

    public Color GetGradeColor(string grade) {
        Color color;
        switch (grade) {
            case "S":
                color = gradeColors[0];
                break;
            case "A":
                color = gradeColors[1];
                break;
            case "B":
                color = gradeColors[2];
                break;
            case "C":
                color = gradeColors[3];
                break;
            default:
                color = gradeColors[4];
                break;
        }
        return color;
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
