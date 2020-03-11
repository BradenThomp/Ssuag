using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment_Microservice.Command.Domain.Repository
{
    public static class RepositoryHelper
    {
        public static object Deserialize(this ResolvedEvent resolvedEvent)
        {
            return JsonConvert
                 .DeserializeObject(Encoding.UTF8.GetString(resolvedEvent.Event.Data), new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects });
        }

        public static byte[] Serialize(this object e)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(e, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Objects }));
        }
    }
}
