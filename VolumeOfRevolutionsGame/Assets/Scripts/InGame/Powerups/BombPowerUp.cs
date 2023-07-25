using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombPowerUp : PowerUp
{
    [SerializeField] GameObject flashObject;
    [SerializeField] Color flashColor;

    private EnemySpawnManager enemySpawnManager;
    private MathFunction mathFunction;

    private bool collected;

    private new void Start() {
        base.Start();
        enemySpawnManager = GameObject.Find("EnemySpawnManager").GetComponent<EnemySpawnManager>();
        mathFunction = GameObject.Find("FunctionManager").GetComponent<MathFunction>();
        collected = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player" && !playerHealth.gameOver && !collected) {
            StartCoroutine(Collected());
        }
    }

    private IEnumerator Collected() {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        collected = true;

        var duration = 0.6f;
        StartCoroutine(ExplosionFlash(duration));
        yield return new WaitForSeconds(duration / 3);
        enemySpawnManager.DestoryAllEnemies();
        mathFunction.DestroyAllCubes();
        yield return new WaitForSeconds(2 * duration);
        Destroy(gameObject);
    }

    private IEnumerator ExplosionFlash(float duration) {
        var flash = Instantiate(flashObject, new Vector3(0, 0, 0), Quaternion.identity);
        
        var image = flash.GetComponent<SpriteRenderer>();
        float r = image.color.r;
        float g = image.color.g;
        float b = image.color.b;

        Color startColor = new Color(r, g, b, 0);
        Color newColor = new Color(r, g, b, 1);

        image.color = startColor;
        for (float t = 0; t < duration / 3; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(startColor, newColor, normalizedTime);
            yield return null;
        }
        
        image.color = newColor;
        yield return new WaitForSeconds(duration / 3);

        for (float t = 0; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(newColor, startColor, normalizedTime);
            yield return null;
        }

        image.color = startColor;
        Destroy(flash);
    }
}
