using Krebsregister.DatenbankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krebsregister.DatenbankModel
{
    public class Eintrag
    {
        public int EintragID { get; set; }
        public int Berichtsjahr { get; set; }
        public int AnzahlMeldungen { get; set; }
       // public int ICD10ID { get; set; }
        //public int BundeslandID { get; set; }
        //public int GeschlechtID  { get; set; }


    }
}
