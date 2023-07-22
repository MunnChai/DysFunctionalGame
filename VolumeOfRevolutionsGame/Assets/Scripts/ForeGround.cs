using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForeGround : MonoBehaviour
{
    [SerializeField] private GameObject foreground;
    [SerializeField] private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        foreground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeInAndOut() {
        foreground.SetActive(true);
        StartCoroutine(FadeTransparencyInAndOut());
    }

    private IEnumerator FadeTransparencyInAndOut() {
        StartCoroutine(FadeTransparency(100, 0.1f));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(FadeTransparency(0, 0.1f));
        yield return new WaitForSeconds(0.1f);
        foreground.SetActive(false);
    }

    private IEnumerator FadeTransparency(float percentage, float duration) {
        float r = image.color.r;
        float g = image.color.g;
        float b = image.color.b;
        float transparency = (percentage / 100);

        Color startColor = image.color;
        Color newColor = new Color(r, g, b, transparency);

        for (float t = 0; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(startColor, newColor, normalizedTime);
            yield return null;
        }
        image.color = newColor; //without this, the value will end at something like 0.9992367
    }
}
