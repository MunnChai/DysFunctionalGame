using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackGround : MonoBehaviour
{
    public Color color;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FadeToColor(Color color, float duration) {

        float r = image.color.r;
        float g = image.color.g;
        float b = image.color.b;

        Color startColor = image.color;
        Color newColor = color;

        for (float t = 0; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(startColor, newColor, normalizedTime);
            yield return null;
        }
        image.color = newColor;
    }
}
