using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApi.Data.Models
{
    public class SignInResult
    {
        public bool Succeeded { get; set; }
        public string Jwt { get; set; }
    }
}
