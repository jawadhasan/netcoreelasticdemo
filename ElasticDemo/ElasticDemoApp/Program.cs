using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

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
                //FindById(client, "1b924e07b05e41328b5fc2c50b0e985e"); //put id here from you document
                // MatchQuery(client, "mention microsoft");// or use one search-word = REST 

                //Perform Match-Phrase Query
                //MatchPhraseQuery(client, "mention microsoft");

                //Perform Query Filter
                //QueryFilter(client);

                //Perform Term Query
                // TermQuery(client, "microsoft");

                //Perform TermRange Query
                //TermRangeQuery(client, "2021-11-30"); //"2015-10-05"


                //Delete Index by name
                //DeleteIndex(client,"blog");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
         
       
        static void FindById(ElasticClient client, string id)
        {
            Console.WriteLine($"**************Executing (FindById) [Match] Query**************");

            var searchResponse = client.Search<Post>(s => s
                                     .Query(q => q
                                             .Match(m => m
                                             .Field(f => f.Id)
                                             .Query(id)
                                             )));

            var docs = searchResponse.Documents;
            WriteToConsole(docs);

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
            WriteToConsole(docs);
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
            WriteToConsole(docs);
        }

        //Filters allow you to reduce the results you returend from elastic-search with logical operators
        static void QueryFilter(ElasticClient client)
        {
            Console.WriteLine($"**************Executing [Search-Query Filter] **************");

            //The Filter method of a bool query takes a params Func<QueryContainerDescriptor<T>, QueryContainer>[] 
            //so that you can pass it multiple expressions to represent multiple filters


            //You can create a list of filters before you make a query if you want to check conditional filters
            var nameList = new[] { "rest", "microsoft" };
            var filters = new List<Func<QueryContainerDescriptor<Post>, QueryContainer>>();
            if (nameList.Any())
            {
                filters.Add(fq => fq.Terms(t => t.Field(f => f.PostContent).Terms(nameList)));
            }


            var searchResponse = client.Search<Post>(s => s
                                        .Query(q => q
                                        .Bool(bq => bq.Filter(filters))));



            //If you don't need to check any condition before making filter query then you can have something like that:
            //var resp = client.Search<Post>(s => s
            //                 .Query(q => q
            //                    .Bool(bq => bq
            //                    .Filter(
            //                            fq => fq.Terms(t => t.Field(f => f.PostContent).Terms(nameList))
            //                            ))));



            //datetime > filters range filters todo...        

            var docs = searchResponse.Documents;
            WriteToConsole(docs);


        }

        static void TermQuery(ElasticClient client, string queryString)
        {
            Console.WriteLine($"**************Executing [Term] Query**************");

            //Search ES for any post with text of "querystring" in it
            var searchResponse = client.Search<Post>(s => s
                                        .Query(q => q
                                                .Term(t => t.PostContent, queryString)));

            var docs = searchResponse.Documents;
            WriteToConsole(docs);
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
            WriteToConsole(docs);
        }

        static void InsertDocument(ElasticClient client)
        {
            Console.WriteLine("Persisting post...");

            //Prepare Entry
            var post = new Post()
            {
                PostDate = new DateTime(2021,12,15),
                PostName = "Another Post",
                PostType = "Communication",
                PostContent = "SignalR allows you to push notification to clients"
            };          

            //Save to Elastic-Search Index
            var indexResponse = client.IndexDocument(post);

            //Console.WriteLine(indexResponse.DebugInformation);
        }          
        static void DeleteIndex(ElasticClient client, string indexName)
        {
           var response = client.Indices.Delete(indexName);
            Console.WriteLine(response.DebugInformation);

        }

        private static void WriteToConsole(IReadOnlyCollection<Post> docs)
        {
            Console.WriteLine($"Document Count: {docs.Count}");
            foreach (var doc in docs)
            {
                WritePost(doc);
            }
        }
        private static void WritePost(Post post)
        {
            Console.WriteLine($"------------------------------------");
            Console.WriteLine($"PostDate: {post.Id}");
            Console.WriteLine($"PostDate: {post.PostDate}");
            Console.WriteLine($"PostName: {post.PostName}");
            Console.WriteLine($"PostType: PostType: {post.PostType}");
            Console.WriteLine($"PostContent: {post.PostContent}");
        }
    }
}
