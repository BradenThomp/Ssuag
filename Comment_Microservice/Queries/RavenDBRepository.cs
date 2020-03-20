using Comment_Microservice.Queries.Entities;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comment_Microservice.Queries
{
    public class RavenDBRepository
    {
        private readonly RavenDBConnection _connection;

        public RavenDBRepository(RavenDBConnection connection)
        {
            _connection = connection;
        }

        public List<Comment> GetCommentsByPost(string PostId)
        {
            IDocumentSession session = _connection.Connect();

            List<Comment> comments = session.Query<Comment>().Where(x => x.PostId.Equals(PostId)).ToList();

            return comments;
        }
    }
}
