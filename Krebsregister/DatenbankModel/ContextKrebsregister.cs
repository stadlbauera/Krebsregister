using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krebsregister.DatenbankModel
{
    public class ContextKrebsregister :DbContext
    {
       public DbSet<Eintrag> Eintrag { get; set; }
        public DbSet<Bundesland> Bundesland { get; set; }
        public DbSet<Geschlecht> Geschlecht { get; set; }
        public DbSet<ICD10> ICD10 { get; set; }

    }
}
