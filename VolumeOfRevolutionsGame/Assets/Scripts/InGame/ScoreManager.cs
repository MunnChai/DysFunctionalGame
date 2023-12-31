using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject levelManagerObject;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text victoryScoreText;
    [SerializeField] private TMP_Text victoryHitsText;
    [SerializeField] private TMP_Text victoryGradeText;
    [SerializeField] public Color[] victoryGradeColors;

    [SerializeField] private static int hitScoreDeduction = 50000;

    public static int score;
    public static int hits;

    private LevelManager levelManager;
    private PlayerHealth playerHealth;
    private SaveData saveData;
    private int scoreGainSpeed;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = levelManagerObject.GetComponent<LevelManager>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        saveData = gameObject.GetComponent<SaveData>();

        score = 0;
        hits = 0;
        scoreGainSpeed = Mathf.RoundToInt(1000000 / levelManager.levelDuration);
    }

    public static void AddScore(int amount) {
        score += amount;
    }

    public static void SubtractScore(int amount) {
        score -= amount;
    }

    public static void AddHit(int amount) {
        hits += amount;
        score -= hitScoreDeduction;
        if (score < 0) {
            score = 0;
        }
    }

    public IEnumerator PassiveScoreGain() {
        yield return null;
        if (!playerHealth.gameOver) {
            score += Mathf.RoundToInt(scoreGainSpeed * Time.deltaTime);
            scoreText.text = score.ToString();
            StartCoroutine(PassiveScoreGain());
        } else {
            CreateVictoryScore();
        }
    }

    private void CreateVictoryScore() {
        victoryScoreText.text = score.ToString();
        victoryHitsText.text = hits.ToString();
        string grade = CalculateGrade();
        Color gradeColor = GetGradeColor(grade);
        victoryGradeText.text = grade;
        victoryGradeText.color = gradeColor;
        UpdateHighScores(LevelSelectMenu.currentLevel.GetName(), grade, score, hits);
        saveData.SaveToJson();
    }

    public string CalculateGrade() {
        string grade;
        float percentage = score / 10000;
        if (percentage > 95) {
            grade = "S";
        } else if (percentage > 90) {
            grade = "A";
            victoryGradeText.color = victoryGradeColors[1];
        } else if (percentage > 80) {
            grade = "B";
            victoryGradeText.color = victoryGradeColors[2];
        } else if (percentage > 70) {
            grade = "C";
            victoryGradeText.color = victoryGradeColors[3];
        } else {
            grade = "D";
            victoryGradeText.color = victoryGradeColors[4];
        }

        return grade;
    }

    public Color GetGradeColor(string grade) {
        Color color;
        switch (grade) {
            case "S":
                color = victoryGradeColors[0];
                break;
            case "A":
                color = victoryGradeColors[1];
                break;
            case "B":
                color = victoryGradeColors[2];
                break;
            case "C":
                color = victoryGradeColors[3];
                break;
            default:
                color = victoryGradeColors[4];
                break;
        }
        return color;
    }

    public static void UpdateHighScores(string levelName, string grade, int score, int hits) {
        foreach (LevelHighscore levelHighscore in SaveData.highScores) {
            if (levelHighscore.levelName == levelName && levelHighscore.score < score) {
                levelHighscore.grade = grade;
                levelHighscore.score = score;
                levelHighscore.hits = hits;
            }
        }
    }
}
