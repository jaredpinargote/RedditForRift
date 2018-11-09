using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sparcopt.Reddit.Api.Domain;
using Sparcopt.Reddit.Api;

public class PostManager: MonoBehaviour {
	private Post post;
	public GameObject postPrefab;

	public void loadPrefab(GameObject prefab, Vector3 pos, Quaternion rot) {
		postPrefab = Instantiate(prefab, pos, rot);
		postPrefab.transform.parent = gameObject.transform;
	}

	public async void loadData(Post p) {
		var redditService = new RedditService();
		post = await redditService.GetPostAsync(p.Id);
		var titleGOT = postPrefab.transform.GetChild(0);
		var titleGO = titleGOT.gameObject;
		titleGO.GetComponent<Text>().text = post.Title;

		var scoreGOT = postPrefab.transform.GetChild(1);
		var scoreGO = scoreGOT.gameObject;
		scoreGO.GetComponent<Text>().text = post.Score.ToString();

		var authorGOT = postPrefab.transform.GetChild(2);
		var authorGO = authorGOT.gameObject;
		authorGO.GetComponent<Text>().text = post.Author;

		var commentsGOT = postPrefab.transform.GetChild(3);
		var commentsGO = commentsGOT.gameObject;
		commentsGO.GetComponent<Text>().text = post.NumberOfComments.ToString() + "comments";
	}
}
