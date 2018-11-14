using UnityEngine;
using UnityEngine.UI;

public class PostDimensions {
    public float width;
    public float height;

    public PostDimensions(GameObject postPrefab) {
        Vector3 scale = postPrefab.transform.localScale;
        var background = postPrefab.transform.Find("background");
        var bgRect = background.GetComponent<RectTransform>();
        width = bgRect.rect.width * scale.x;
        height = bgRect.rect.height * scale.y;
    }
}