using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    private Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = gameObject.GetComponent<Tilemap>();

        tileMap.color = LevelSelectMenu.currentLevel.GetColor();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(-scrollSpeed * 1.6f * Time.deltaTime, scrollSpeed * Time.deltaTime, 0);

        if (transform.position.y <= -160) {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
