﻿using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Implementation
{
    public class Token : IToken
    {
        public string Type { get; set; }
        public string Username { get; set; }
        public string Application_name { get; set; }
        public string Client_id { get; set; }
        public string Token_type { get; set; }
        public string Access_token { get; set; }
        public int Expires_in { get; set; }
        public string State { get; set; }
        public string Scope { get; set; }
    }
}
