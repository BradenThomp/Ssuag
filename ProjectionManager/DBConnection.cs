using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;

namespace ProjectionManager

{
    internal class DBConnection
    {
        private readonly IDocumentStore _store;

        public DBConnection(string database)
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
