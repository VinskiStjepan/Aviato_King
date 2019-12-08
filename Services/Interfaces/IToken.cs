using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IToken
    {
        string Type { get; set; }
        string Username { get; set; }
        string Application_name { get; set; }
        string Client_id { get; set; }
        string Token_type { get; set; }
        string Access_token { get; set; }
        int Expires_in { get; set; }
        string State { get; set; }
        string Scope { get; set; }
    }
}
