using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MathFunction : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float curveWidth;
    [SerializeField] private float acceleration;

    public float speed;

    private float currentX;
    private ArrayList cubes = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        currentX = 0;
        speed = 32;
    }

    public IEnumerator DrawCubes() {
        yield return new WaitForSeconds(0.2f);
        float yPos = PickFunction(currentX) * (Constants.topBound - Constants.bottomBound) / 2 + 20;
        var cubeObject = Instantiate(cube, new Vector3(Constants.rightBound + cube.transform.localScale.x / 2, yPos, 0), Quaternion.identity);
        var image = cubeObject.GetComponent<SpriteRenderer>();
        try {
            image.color = LevelSelectMenu.currentLevel.GetColor();
        } catch (Exception e) {
            // Pass
        }
        cubeObject.transform.parent = gameObject.transform;
        cubes.Add(cubeObject);
        currentX += speed / (curveWidth * Mathf.PI);
        speed += acceleration * Time.deltaTime;
        StartCoroutine(DrawCubes());
    }

    public void DestroyAllCubes() {
        foreach (GameObject cube in cubes) {
            Destroy(cube);
        }
    }

    private float PickFunction(float x) {
        float num;
        string levelName = "";
        try {
            levelName = LevelSelectMenu.currentLevel.GetName();
        } catch (Exception e) {
            // Pass
        }
        
        switch (levelName) {
            case "Sin(x)":
                num = Mathf.Sin(x);
                break;
            case "Cos(x)":
                num = Mathf.Cos(x);
                break;
            case "Tan(x)":
                try {
                    num = Mathf.Tan(x) / 2;
                } catch (Exception e) {
                    num = 0;
                }
                break;
            case "Sin(x)+Cos(2x+2)":
                num = (Mathf.Sin(x) + Mathf.Cos(2 * x + 2)) / 2;
                break;
            default:
                num = 0;
                break;
        }
        return num;
    }
}
