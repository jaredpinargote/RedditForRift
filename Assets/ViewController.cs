using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sparcopt.Reddit.Api;

public class ViewController : MonoBehaviour {
	private List<PostManager> postManagers;
	public GameObject postPreFab;

	// Use this for initialization
	void Start () {
		loadSubredditPosts();
	}

	async void loadSubredditPosts() {
		var redditService = new RedditService();
		var subReddit = await redditService.GetSubredditAsync("all");
		var position = new Vector3(0f, 0f, 0f);
		GameObject subRedditPostsGO = new GameObject();
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
