using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level
{
    private Image graphPreview;
    private string function;
    
    public Level(string function) {
        this.function = function;
    }

    public string GetFunction() {
        return function;
    }
}
