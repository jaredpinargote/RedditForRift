using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sparcopt.Reddit.Api;

public class ViewController : MonoBehaviour {
	private List<PostManager> postManagers;
	private GameObject subRedditPostsGO;
	private const int framesToCheckPos = 30;
	private int framesChecked = 0;
	// Public Properties
	public GameObject postPreFab;


	// Use this for initialization
	private void Start () {
		loadSubredditPosts();
	}

	private void Update() {
		if (framesChecked > framesToCheckPos) { return; }
		setPosition();
		framesChecked++;
	}

	void setPosition() {
		var camera = GameObject.FindGameObjectWithTag("MainCamera");
		var pos = camera.transform.position;
		pos.z += 0.9f;
		// if (subRedditPostsGO == null) { return; }
		subRedditPostsGO.transform.position = pos;
	}

	async void loadSubredditPosts() {
		var redditService = new RedditService();
		var subReddit = await redditService.GetSubredditAsync("all");
		var position = new Vector3(0f, 0f, 0f);
		subRedditPostsGO = new GameObject();
		subRedditPostsGO.name = "Subreddit Posts";
		foreach (var p in subReddit.Posts) {
			GameObject postGO = new GameObject();
			postGO.name = p.Id;
			postGO.transform.parent = subRedditPostsGO.transform;
			postGO.transform.position = position;
			PostManager po = postGO.AddComponent<PostManager>();
			po.loadPrefab(postPreFab, position, Quaternion.identity);
			po.loadData(p);
			position.z += 0.9f;
		}
	}
}
