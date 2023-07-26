using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private int targetFPS = -1;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = targetFPS;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.targetFrameRate != targetFPS) {
            Application.targetFrameRate = targetFPS;
        }
    }
}
