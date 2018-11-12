using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sparcopt.Reddit.Api;

public class PostObjectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// test comment
		testRedditCall();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	async void testRedditCall() {
		var redditService = new RedditService();

		var subRedditList = await redditService.GetSubredditsAsync();
		var subReddit = await redditService.GetSubredditAsync("all");

		var subRedditPosts = subReddit.Posts;
		var post = await redditService.GetPostAsync(subRedditPosts[0].Id);

		var title = post.Title;
		var score = post.Score;

		Debug.Log(title + "\n" + score);

		var titleGOT = this.gameObject.transform.GetChild(0);
		var titleGO = titleGOT.gameObject;
		titleGO.GetComponent<Text>().text = title;

		var scoreGOT = this.gameObject.transform.GetChild(1);
		var scoreGO = scoreGOT.gameObject;
		scoreGO.GetComponent<Text>().text = score.ToString();
	}
}
