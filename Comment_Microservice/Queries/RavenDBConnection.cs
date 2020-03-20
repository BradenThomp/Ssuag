using Comment_Microservice.Queries.Entities;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Queries
{
    public class RavenDBConnection
    {
        private readonly IDocumentStore _store;

        public RavenDBConnection(string database)
        {
            _store = new DocumentStore
            {
                Urls = new[] { "http://localhost:8080/", },
                Database = database
            };

            _store.Initialize();
        }

        public IDocumentSession Connect()
        {
            return _store.OpenSession();
        }

    }
}
