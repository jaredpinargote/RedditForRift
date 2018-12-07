using UnityEngine;
using UnityEngine.UI;

public class PostDimensions {
    public float width;
    public float height;

    public PostDimensions(GameObject postPrefab) {
        var bgRect = postPrefab.GetComponent<RectTransform>();
        var scale = bgRect.localScale;
        width = bgRect.rect.width * scale.x;
        height = bgRect.rect.height * scale.y;
    }
}