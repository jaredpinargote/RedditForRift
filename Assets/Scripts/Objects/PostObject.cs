using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
using Sparcopt.Reddit.Api.Domain;

public class PostObject {
	private Post post;
	private Text text;
	private GameObject gameObject;

	// Public Parameters
	public int fontSize = 300;
	public int minFontSize = 1;
	public int maxFontSize = 300;
	public Color textColor = Color.white;
	public float canvasScalerReferencePixelsPerUnit = 100;
	public Vector2 titleCanvasSize = new Vector2(800f, 400f);
	public Vector2 scoreCanvasSize = new Vector2(200f, 200f);
	public Vector3 canvasScale = new Vector3(0.002f, 0.002f, 0f);

	public PostObject(Post p, Vector3 pos) {
		gameObject = new GameObject();
		gameObject.name = "Post" + post.Id;
		gameObject.transform.position = pos;
		post = p;
		createPostTitleCanvas();
	}

	private void createPostTitleCanvas() {
		GameObject postTitleGO = new GameObject();
		postTitleGO.transform.position = gameObject.transform.position;
		postTitleGO.transform.parent = gameObject.transform;
		createCanvasComponents(titleCanvasSize, canvasScale, postTitleGO);
		// Text
		Text t = createTextComponent(postTitleGO);
		t.text = post.Title;
	}

	private void createPostScoreCanvas() {
		GameObject postScoreGO = new GameObject();
		Vector3 goPos = gameObject.transform.position;
		goPos.x -= 1.053f;
		postScoreGO.transform.position = goPos;
		postScoreGO.transform.parent = gameObject.transform;
		createCanvasComponents(titleCanvasSize, canvasScale, postScoreGO);
		// Text
		Text t = createTextComponent(postScoreGO);
		t.text = post.Title;
	}

	private void createCanvasComponents(Vector2 size, Vector3 scale, GameObject go) {
		Canvas canvas = go.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.WorldSpace;
		CanvasScaler cs = go.AddComponent<CanvasScaler>();
		cs.referencePixelsPerUnit = canvasScalerReferencePixelsPerUnit;
		cs.dynamicPixelsPerUnit = 1f;
		GraphicRaycaster gr = go.AddComponent<GraphicRaycaster>();
		gr.ignoreReversedGraphics = true;
		RectTransform rt = go.GetComponent<RectTransform>();
		rt.position = go.transform.position;
		rt.sizeDelta = size;
		rt.localScale = scale;
	}

	private Text createTextComponent(GameObject go) {
		Text t = go.AddComponent<Text>();
		t.alignment = TextAnchor.MiddleCenter;
		t.horizontalOverflow = HorizontalWrapMode.Wrap;
		t.verticalOverflow = VerticalWrapMode.Truncate;
		Font ArialFont = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		t.font = ArialFont;
		t.fontSize = fontSize;
		t.resizeTextForBestFit = true;
		t.resizeTextMinSize = minFontSize;
		t.resizeTextMaxSize = maxFontSize;
		t.enabled = true;
		t.color = textColor;
		return t;
	}
}
