using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBackGround : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float menuMovementMultiplier;

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
        MoveWithMouse();
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

    private void MoveWithMouse() {
        Vector2 mouseTruePosition = camera.WorldToViewportPoint(Input.mousePosition);

        //now you can set the position of the ui element
        gameObject.GetComponent<RectTransform>().anchoredPosition = mouseTruePosition * menuMovementMultiplier;
    }
}
