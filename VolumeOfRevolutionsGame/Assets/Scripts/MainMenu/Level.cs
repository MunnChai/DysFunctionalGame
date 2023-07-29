using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level
{
    private Image graphPreview;
    private string name;
    private Color color;
    private int difficulty;
    private GameObject[] enemies;
    
    public Level(string name, Color color, int difficulty, params GameObject[] enemies) {
        this.name = name;
        this.color = color;
        this.difficulty = difficulty;
        this.enemies = enemies;
    }

    public string GetName() {
        return name;
    }

    public Color GetColor() {
        return color;
    }

    public int GetDifficulty() {
        return difficulty;
    }

    public GameObject[] GetEnemies() {
        return enemies;
    }
}
