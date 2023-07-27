using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPreview : MonoBehaviour
{
    [SerializeField] private int numGraphSegments;
    [SerializeField] private GameObject graphSegmentPrefab;
    [SerializeField] private Camera camera;
    [SerializeField] private float xBound;

    private ArrayList graphSegments = new ArrayList();
    private RectTransform graphSegmentTransform;
    private RectTransform imageTransform;
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        graphSegmentTransform = graphSegmentPrefab.GetComponent<RectTransform>();
        imageTransform = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator UpdatePreview(Level level) {
        yield return null;
        ClearGraph();

        string levelName = level.GetName();
        float currentX = -xBound;
        float deltaX = 2 * xBound / numGraphSegments;

        float imageWidth = imageTransform.sizeDelta.x * imageTransform.localScale.x;
        graphSegmentTransform.sizeDelta = new Vector2(imageWidth / numGraphSegments, imageWidth / numGraphSegments);

        for (int i = 0; i < numGraphSegments; i++) {
            GameObject segment = Instantiate(graphSegmentPrefab, new Vector2(0, 0), Quaternion.identity);
            segment.transform.parent = gameObject.transform;
            float xPos = CalculateXPos(i);
            float yPos = CalculateYPos(currentX, level);
            segment.GetComponent<RectTransform>().anchoredPosition = camera.WorldToViewportPoint(new Vector2(xPos * 17, yPos * 17));
            graphSegments.Add(segment);
            currentX += deltaX;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void ClearGraph() {
        foreach (GameObject graphSegment in graphSegments) {
            Destroy(graphSegment);
        }
    }

    private float CalculateXPos(int index) {
        float imageWidth = imageTransform.sizeDelta.x * imageTransform.localScale.x;
        float graphSegmentWidth = graphSegmentTransform.sizeDelta.x * graphSegmentTransform.localScale.x;

        float xPos = (index * graphSegmentWidth) + graphSegmentWidth / 2 - imageWidth/2;
        return xPos;
    }

    private float CalculateYPos(float x, Level level) {
        float y;
        switch (level.GetName()) {
            case "Sin(x)":
                y = Mathf.Sin(x);
                break;
            case "Cos(x)":
                y = Mathf.Cos(x);
                break;
            case "Tan(x)":
                y = Mathf.Tan(x);
                break;
            case "Sin(x)+Cos(2x+2)":
                y = Mathf.Sin(x) + Mathf.Cos(2*x + 2);
                break;
            default:
                y = 0;
                break;
        }
        y *= 10;
        return y;
    }
}
