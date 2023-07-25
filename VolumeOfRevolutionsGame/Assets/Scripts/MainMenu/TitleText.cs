using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleText : MonoBehaviour
{
    private float currentX;
    // Start is called before the first frame update
    void Start()
    {
        currentX = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentX += Mathf.PI / 4 * Time.deltaTime;
        var deltaY = Mathf.Sin(currentX) / 50;
        transform.position += new Vector3(0, deltaY, 0);
    }
}
