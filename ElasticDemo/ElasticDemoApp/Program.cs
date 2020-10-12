using Nest;
using System;

namespace ElasticDemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //1. Setup Connection and ElasticClient
                Console.WriteLine("Connecting to Elastic-Search");
                var node = new Uri("http://localhost:9200");

                var setting = new ConnectionSettings(node)
                    .DefaultIndex("blog");

                var client = new ElasticClient(setting);

                //Insert Posts
                //InsertDocument(client);

                //Perform Match Query
                //MatchQuery(client, "async");// or use one search-word = REST 

                //Perform Match-Phrase Query
                //MatchPhraseQuery(client, "REST API");

                //Perform TermRange Query
                //TermRangeQuery(client, "2015-10-05");

                //Perform Term Query
                //TermQuery(client, "azure");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
         
       




        static void TermQuery(ElasticClient client, string queryString)
        {
            Console.WriteLine($"**************Executing [Term] Query**************");

            //Search ES for any post with text of "querystring" in it
            var searchResponse = client.Search<Post>(s => s                                        
                                        .Query(q => q
                                                .Term(t => t.PostContent,queryString)));

            var docs = searchResponse.Documents;
            Console.WriteLine($"Document Count: {docs.Count}");

            foreach (var doc in docs)
            {
                WritePost(doc);
            }
        }

        static void MatchPhraseQuery(ElasticClient client, string queryString)
        {
            Console.WriteLine($"**************Executing [MatchPhrase] Query**************");

            var searchResponse = client.Search<Post>(s => s                                       
                                        .Query(q => q
                                                .MatchPhrase(m => m
                                                .Field(f => f.PostContent)
                                                .Query(queryString)
                                                )));

            var docs = searchResponse.Documents;
            Console.WriteLine($"Document Count: {docs.Count}");

            foreach (var doc in docs)
            {
                WritePost(doc);
            }
        }

        static void MatchQuery(ElasticClient client, string queryString)
        {
            Console.WriteLine($"**************Executing [Match] Query**************");

            var searchResponse = client.Search<Post>(s => s                                     
                                        .Query(q => q
                                                .Match(m => m
                                                .Field(f => f.PostContent)
                                                .Query(queryString)
                                                )));

            var docs = searchResponse.Documents;
            Console.WriteLine($"Document Count: {docs.Count}");

            foreach(var doc in docs)
            {
                WritePost(doc);
            }
        }

        static void TermRangeQuery(ElasticClient client, string queryString)
        {
            Console.WriteLine($"**************Executing [Range] Query**************");

            var searchResponse = client.Search<Post>(s => s
                                        .Query(q =>
                                            q.TermRange(t =>
                                                t.Field(p => p.PostDate)
                                                .GreaterThan(queryString))));
                                       

            var docs = searchResponse.Documents;
            Console.WriteLine($"Document Count: {docs.Count}");

            foreach (var doc in docs)
            {
                WritePost(doc);
            }
        }

        static void InsertDocument(ElasticClient client)
        {
            Console.WriteLine("Persisting post...");

            //Prepare Entry
            var post = new Post()
            {
                PostDate = new DateTime(2020,10,11),
                PostName = "REST Async api",
                PostType = "Communication",
                PostContent = "REST Async API Discussion"
            };          

            //Save to Elastic-Search Index
            var indexResponse = client.IndexDocument(post);

            //Console.WriteLine(indexResponse.DebugInformation);
        }


        private static void WritePost(Post post)
        {
            Console.WriteLine($"------------------------------------");
            Console.WriteLine($"PostDate: {post.PostDate}");
            Console.WriteLine($"PostName: {post.PostName}");
            Console.WriteLine($"PostType: PostType: {post.PostType}");
            Console.WriteLine($"PostContent: {post.PostContent}");
        }
    }
}
