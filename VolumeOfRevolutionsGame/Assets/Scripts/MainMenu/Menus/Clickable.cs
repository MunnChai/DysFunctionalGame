using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{

    private bool onCooldown = false;

    public void Click() {
        if (!onCooldown) {
            StartCoroutine(ClickAnimation(0.1f));
            StartCoroutine(StartCooldown(0.1f));
        }   
    }

    private IEnumerator ClickAnimation(float duration) {
        var initialScale = transform.localScale;
        float scaleSpeed = 0.005f;
        for (float t = 0; t < duration; t+= Time.deltaTime) {
            if (t < duration / 2) {
                transform.localScale -= new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
            } else {
                transform.localScale += new Vector3(scaleSpeed, scaleSpeed, scaleSpeed);
            }
            yield return null;
        }
        transform.localScale = initialScale;
    }

    private IEnumerator StartCooldown(float seconds) {
        onCooldown = true;
        yield return new WaitForSeconds(seconds);
        onCooldown = false;
    }
}
