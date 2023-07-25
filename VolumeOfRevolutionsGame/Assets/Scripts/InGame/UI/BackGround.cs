using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private Image image;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        color = image.color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Sets transparency to given percentage
    public void SetTransparency(float percentage) {
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float transparency = (percentage / 100);

        image.color = new Color(r, g, b, transparency);
    }

    public IEnumerator FadeTransparency(float percentage, float duration) {
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float transparency = (percentage / 100);

        Color startColor = image.color;
        Color newColor = new Color(r, g, b, transparency);

        for (float t = 0; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(startColor, newColor, normalizedTime);
            yield return null;
        }
        image.color = newColor; 
    }
}
