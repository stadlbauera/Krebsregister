using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krebsregister.DatenbankModel
{
    public class Bundesland 
    {
        public int BundeslandID { get; set; }
        public string NameBundesland { get; set; }
    }
}
