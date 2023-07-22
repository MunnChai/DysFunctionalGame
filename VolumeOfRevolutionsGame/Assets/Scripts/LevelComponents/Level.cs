using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level
{
    private Image graphPreview;
    private string name;
    
    public Level(string name) {
        this.name = name;
    }

    public string GetName() {
        return name;
    }
}
