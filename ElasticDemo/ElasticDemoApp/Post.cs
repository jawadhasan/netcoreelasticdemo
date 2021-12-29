using System;

namespace ElasticDemoApp
{
    public class Post
    {
        public string Id { get; set; }
        public string PostName { get; set; }
        public string PostType { get; set; }
        public string PostContent { get; set; }
        public DateTime? PostDate { get; set; }

        public Post()
        {
            Id = Guid.NewGuid().ToString("N");
        }

    }
}
