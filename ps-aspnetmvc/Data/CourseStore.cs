using Microsoft.Azure.Documents.Client;
using ps_aspnetmvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ps_aspnetmvc.Data
{
    public class CourseStore
    {
        DocumentClient _client;
        Uri _coursesLink;

        public CourseStore()
        {
            _client = new DocumentClient(new Uri("https://ps-harko-docdb.documents.azure.com:443/"),
                "nftoxx7ht0govV52JRCKk2uoI8yRteoXFuGdCL7bg0Wk5ApigmQYqgreKS6AmH0qASED9amJC9nRfqbkaoDiXg==");

            _coursesLink = UriFactory.CreateDocumentCollectionUri("coursedb", "courses");
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _client.CreateDocumentQuery<Course>(_coursesLink)
                            .OrderBy(c => c.Title);
        }

        public async Task InsertCourses(IEnumerable<Course> courses)
        {
            foreach (var course in courses)
            {
                await _client.CreateDocumentAsync(_coursesLink, course);
            }
        }

        
    }
}