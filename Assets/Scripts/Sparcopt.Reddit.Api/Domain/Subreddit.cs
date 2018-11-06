using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparcopt.Reddit.Api.Domain
{
    public class Subreddit
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}
