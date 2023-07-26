using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private SerializableList<LevelHighscore> serializableHighScores;

    public static List<LevelHighscore> highScores = new List<LevelHighscore>() {
        new LevelHighscore("Sin(x)", "D", 0, 0), 
        new LevelHighscore("Cos(x)", "D", 0, 0), 
        new LevelHighscore("Tan(x)", "D", 0, 0), 
        new LevelHighscore("Sin(x)+Cos(2x+2)", "D", 0, 0)
    };

    public void SaveToJson() {
        serializableHighScores.list = highScores;

        string saveData = JsonUtility.ToJson(serializableHighScores);
        string filePath = Application.persistentDataPath + "/HighScoreData.json";
        Debug.Log(filePath);

        System.IO.File.WriteAllText(filePath, saveData);
        Debug.Log(saveData);
        Debug.Log("Saved");
    }

    public void LoadFromJson() {
        string filePath = Application.persistentDataPath + "/HighScoreData.json";
        string saveData = System.IO.File.ReadAllText(filePath);
        highScores = JsonUtility.FromJson<SerializableList<LevelHighscore>>(saveData).list;
        Debug.Log("Loaded");
    }
}

[System.Serializable]
public class SerializableList<LevelHighscore> {
    public List<LevelHighscore> list;
}

[System.Serializable]
public class LevelHighscore
{
    public string levelName;
    public string grade;
    public int score;
    public int hits;

    public LevelHighscore(string levelName, string grade, int score, int hits) {
        this.levelName = levelName;
        this.grade = grade;
        this.score = score;
        this.hits = hits;
    }
}
