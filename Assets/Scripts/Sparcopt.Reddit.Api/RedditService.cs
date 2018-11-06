using Sparcopt.Reddit.Api.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Sparcopt.Reddit.Api
{
    public class RedditService
    {
        HttpClient client;
        public RedditService()
        {
            client = new HttpClient();
        }

        public async Task<IList<string>> GetSubredditsAsync()
        {
            var subreddits = new List<string>();

            var response = await client.GetStringAsync("http://www.reddit.com/.json");

            var jObject = JObject.Parse(response);

            var jArray = jObject["data"]["children"] as JArray;

            foreach (var obj in jArray)
            {
                subreddits.Add(obj["data"]["subreddit"].ToString());
            }

            subreddits = subreddits.OrderBy(sr => sr).ToList();

            return subreddits;
        }

        public async Task<Subreddit> GetSubredditAsync(string name)
        {
            var subreddit = new Subreddit();

            subreddit.Name = name;

            subreddit.Posts = new List<Post>();

            var response = await client.GetStringAsync("http://www.reddit.com/r/" + name + "/.json");

            var jObject = JObject.Parse(response);

            var jArray = jObject["data"]["children"] as JArray;

            foreach (var obj in jArray)
            {
                var post = new Post();

                post.Id = obj["data"]["id"].ToString();
                post.Title = obj["data"]["title"].ToString();
                post.Author = obj["data"]["author"].ToString();
                post.Score = obj["data"]["score"].Value<int>();
                post.Thumbnail = obj["data"]["thumbnail"].ToString();
                post.Url = obj["data"]["url"].ToString();
                post.NumberOfComments = obj["data"]["num_comments"].Value<int>();

                subreddit.Posts.Add(post);
            }

            return subreddit;
        }

        public async Task<IList<string>> SearchForSubredditsAsync(string query)
        {
            var subreddits = new List<string>();

            var response = await client.GetStringAsync("http://www.reddit.com/subreddits/search.json?q=" + query);

            var jObject = JObject.Parse(response);

            var jArray = jObject["data"]["children"] as JArray;

            foreach (var obj in jArray)
            {
                subreddits.Add(obj["data"]["display_name"].ToString());
            }

            return subreddits;
        }

        public async Task<Post> GetPostAsync(string id)
        {
            var post = new Post();
            var response = await client.GetStringAsync("http://www.reddit.com/api/info.json?id=t3_" + id);
            var jObject = JObject.Parse(response);
            var obj = (jObject["data"]["children"] as JArray)[0];
            post.Id = obj["data"]["id"].ToString();
            post.Title = obj["data"]["title"].ToString();
            post.Author = obj["data"]["author"].ToString();
            post.Score = obj["data"]["score"].Value<int>();
            post.Thumbnail = obj["data"]["thumbnail"].ToString();
            post.Url = obj["data"]["url"].ToString();
            post.NumberOfComments = obj["data"]["num_comments"].Value<int>();
            return post;
        }
    }
}
