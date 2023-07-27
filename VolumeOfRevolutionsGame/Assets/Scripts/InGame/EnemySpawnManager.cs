using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private float spawnStartInterval;
    [SerializeField] private float spawnIntervalIncrease;
    [SerializeField] private float lowestSpawnInterval;

    [SerializeField] private GameObject playerObject;

    public ArrayList enemies = new ArrayList();

    private PlayerHealth playerHealthScript;

    // Start is called before the first frame update
    void Start()
    {
        playerHealthScript = playerObject.GetComponent<PlayerHealth>();
        StartCoroutine(SpawnEnemy(spawnStartInterval));
    }

    public void DestoryAllEnemies() {
        foreach (GameObject enemy in enemies) {
            Destroy(enemy);
        }
    }

    private IEnumerator SpawnEnemy(float interval) {
        yield return new WaitForSeconds(interval);
        float yPos;
        float randomNum = Random.Range((int) 0, (int) 2);
        if (randomNum == 0) {
            yPos = Constants.topBound + 20;
        } else {
            yPos = Constants.bottomBound - 20;
        }

        int randomEnemyIndex = Random.Range((int) 0, (int) enemyTypes.Length);

        GameObject newEnemy = Instantiate(enemyTypes[randomEnemyIndex], 
                                        new Vector3(Random.Range(Constants.leftBound, Constants.rightBound), yPos, 0), 
                                        Quaternion.identity);

        enemies.Add(newEnemy);
        newEnemy.transform.parent = gameObject.transform;
        
        if (!playerHealthScript.gameOver) {
            float newInterval = interval - spawnIntervalIncrease;
            if (newInterval < lowestSpawnInterval) {
                newInterval = lowestSpawnInterval;
            }
            StartCoroutine(SpawnEnemy(newInterval));
        } else {
            DestoryAllEnemies();
        }
    }
}
