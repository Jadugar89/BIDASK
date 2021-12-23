using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIDASK.Shared
{
    public class CommandtoApi
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string Symbol { get; set; }
        public double sl { get; set; } = 0.0;
        public double tp { get; set; } = 0.0;
        public double volume { get; set; } = 0.1;

        public CommandtoApi(string ID, string Password, string Symbol)
        {
            this.ID = ID;
            this.Password = Password;
            this.Symbol = Symbol;
        }
    }

}
