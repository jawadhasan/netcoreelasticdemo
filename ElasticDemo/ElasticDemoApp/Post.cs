using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticDemoApp
{
    public class Post
    {
        public string PostName { get; set; }
        public string PostType { get; set; }
        public string PostContent { get; set; }
        public DateTime? PostDate { get; set; }

        public Post()
        {
        }

    }
}
