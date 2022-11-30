using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krebsregister
{
    internal class TestEintrag
    {
        public String ICD10 { get; set; }
        public int Berichtsjahr { get; set; }
        public String Bundesland { get; set; }
        public String Geschlecht { get; set; }
        public int AnzahlMeldungen { get; set; }

        public override string ToString()
        {
            return $"{ICD10} {Berichtsjahr} {Bundesland} {Geschlecht} {AnzahlMeldungen}";
        }
    }
}
