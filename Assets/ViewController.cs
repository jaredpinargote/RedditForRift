using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sparcopt.Reddit.Api;

public class ViewController : MonoBehaviour {
	private List<PostObject> postObjects;

	// Use this for initialization
	void Start () {
		loadSubredditPosts();
	}

	async void loadSubredditPosts() {
		var redditService = new RedditService();
		var subReddit = await redditService.GetSubredditAsync("all");
		var position = new Vector3(0f, 0f, 0f);
		foreach (var p in subReddit.Posts) {
			var post = await redditService.GetPostAsync(p.Id);
			PostObject postObject = new PostObject(post, position);
			position.y += 0.82f;
			// postObjects.Add(postObject);
		}
	}
}
