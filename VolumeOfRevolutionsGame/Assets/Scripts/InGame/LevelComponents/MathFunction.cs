using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathFunction : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float curveWidth;
    [SerializeField] private float acceleration;

    public float speed;

    private float currentX;

    // Start is called before the first frame update
    void Start()
    {
        currentX = 0;
        speed = 32;
        InvokeRepeating("DrawCubes", 3.0f, 0.2f);
    }

    public void DrawCubes() {
        float yPos = Mathf.Sin(currentX) * (Constants.topBound - Constants.bottomBound) / 2 + 20;
        Instantiate(cube, new Vector3(Constants.rightBound + cube.transform.localScale.x / 2, yPos, 0), Quaternion.identity);
        currentX += speed / (curveWidth * Mathf.PI);
        speed += acceleration * Time.deltaTime;
    }
}
