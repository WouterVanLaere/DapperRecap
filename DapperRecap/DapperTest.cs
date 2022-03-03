using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperRecap
{
    public class DapperTest
    {
        public int? Id { get; set; }            //int? want anders toont het een 0 in plaats van niets
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }           //int? want anders toont het een 0 in plaats van niets
        public string Gender { get; set; }

        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {Age} {Gender}";
        }
    }
}
