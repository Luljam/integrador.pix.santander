using Newtonsoft.Json;
using System.Collections.Generic;

namespace Phidelis.Integracao.Teams.Rest.Models.Get
{
    public class Token
    {
        public string refreshUrl { get; set; }
        public string token_type { get; set; }
        public string client_id { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string expires_in { get; set; }
    }
}
