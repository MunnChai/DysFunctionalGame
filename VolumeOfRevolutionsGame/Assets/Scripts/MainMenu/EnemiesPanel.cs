using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyImages;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePanel() {
        var levelEnemies = LevelSelectMenu.currentLevel.GetEnemies();

        for (int i = 0; i < enemyImages.Length; i++) {
            var sprite = levelEnemies[i].GetComponent<SpriteRenderer>().sprite;
            enemyImages[i].GetComponent<Image>().sprite = sprite;
        }
    }
}
