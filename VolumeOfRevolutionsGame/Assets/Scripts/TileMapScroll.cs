using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(-scrollSpeed * 1.6f * Time.deltaTime, scrollSpeed * Time.deltaTime, 0);

        if (transform.position.y <= -10) {
            transform.position = new Vector3(0, 0, 0);
        }
    }
}
