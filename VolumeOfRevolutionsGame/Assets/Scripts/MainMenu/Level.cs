using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level
{
    private Image graphPreview;
    private string name;
    private Color color;
    
    public Level(string name, Color color) {
        this.name = name;
        this.color = color;
    }

    public string GetName() {
        return name;
    }

    public Color GetColor() {
        return color;
    }
}
