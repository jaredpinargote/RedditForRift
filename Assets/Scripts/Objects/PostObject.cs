using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
using Sparcopt.Reddit.Api.Domain;

public class PostObject {
	private Post post;
	private Text text;
	private GameObject gameObject;
	public PostObject(Post p, Vector3 pos) {
		gameObject = new GameObject();
		gameObject.transform.position = pos;
		post = p;
		createPostTitleCanvas();
	}

	void createPostTitleCanvas() {
		Canvas canvas = gameObject.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.WorldSpace;
		CanvasScaler cs = gameObject.AddComponent<CanvasScaler>();
		cs.referencePixelsPerUnit = 100f;
		cs.dynamicPixelsPerUnit = 1f;
		GraphicRaycaster gr = gameObject.AddComponent<GraphicRaycaster>();
		gr.ignoreReversedGraphics = true;
		RectTransform rt = gameObject.GetComponent<RectTransform>();
		rt.position = gameObject.transform.position;
		// rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.002f);
		// rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0.002f);
		rt.sizeDelta = new Vector2(800f, 400f);
		rt.localScale = new Vector3(0.002f, 0.002f, 0f);

		// Text
		Text t = gameObject.AddComponent<Text>();
		t.alignment = TextAnchor.MiddleCenter;
		t.horizontalOverflow = HorizontalWrapMode.Wrap;
		t.verticalOverflow = VerticalWrapMode.Truncate;
		Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		t.font = ArialFont;
		t.fontSize = 300;
		t.resizeTextForBestFit = true;
		t.resizeTextMinSize = 1;
		t.resizeTextMaxSize = 300;
		t.text = post.Title;
		t.enabled = true;
		t.color = Color.white;

		gameObject.name = "Post Title";
	}
}
