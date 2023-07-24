using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject xEnemy, xSquaredEnemy, xCubedEnemy;

    [SerializeField] private float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    private void SpawnEnemy() {

        float yPos;
        float randomNum = Random.Range((int) 0, (int) 2);
        if (randomNum == 0) {
            yPos = Constants.topBound + 20;
        } else {
            yPos = Constants.bottomBound - 20;
        }

        GameObject newEnemy = Instantiate(xEnemy, new Vector3(Random.Range(Constants.leftBound, Constants.rightBound), 
                                                              yPos, 0), Quaternion.identity);
    }
}
