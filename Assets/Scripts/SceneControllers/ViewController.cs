using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sparcopt.Reddit.Api;

public class ViewController : MonoBehaviour {
	private List<PostManager> postManagers;
	private GameObject subRedditPostsGO;
	private const int framesToCheckPos = 1;
	private int framesChecked = 0;
	private Vector3 gridCameraOffset;
	private PostDimensions postDimensions;
	private float bufferBetweenPosts = 0.005f;

	// Public Properties
	public GameObject postPreFab;


	// Use this for initialization
	private void Start () {
		loadSubredditPosts();
		loadPostDimensions();
	}

	private void Update() {
		if (framesChecked > framesToCheckPos) { return; }
		setPosition();
		framesChecked++;
	}

	private void setPosition() {
		var camera = GameObject.FindGameObjectWithTag("MainCamera");
		var pos = camera.transform.position;
		pos += gridCameraOffset;
		subRedditPostsGO.transform.position = pos;
	}

	private void loadPostDimensions() {
		postDimensions = new PostDimensions(postPreFab);
		gridCameraOffset = new Vector3(
			-(postDimensions.width + bufferBetweenPosts),
			0f,
			0.7f
		);
	}

	async void loadSubredditPosts() {
		var redditService = new RedditService();
		var subReddit = await redditService.GetSubredditAsync("all");
		var position = new Vector3(0f, 0f, 0f);
		subRedditPostsGO = new GameObject();
		subRedditPostsGO.name = "Subreddit Posts";
		int count = 0;
		float w = postDimensions.width + bufferBetweenPosts;
		float h = postDimensions.height + bufferBetweenPosts; 
		foreach (var p in subReddit.Posts) {
			GameObject postGO = new GameObject();
			postGO.name = p.Id;
			postGO.transform.parent = subRedditPostsGO.transform;
			postGO.transform.position = position;
			PostManager po = postGO.AddComponent<PostManager>();
			int wMultiplier = (count % 3);
			int hMultiplier = count / 3;
			position.x =  w * wMultiplier;
			position.y =  h * hMultiplier;
			po.loadPrefab(postPreFab, position, Quaternion.identity);
			po.loadData(p);
			count++;
		}
	}
}
