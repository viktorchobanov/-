using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitsConversion
{
    public class Unit
    {

        public string Name { get; set; }
        public UnitType Type { get; set; }
        public string ToName { get; set; }
        public string Formula { get; set; }

        public override string ToString()
        {
            return string.Format("{0}",
                                 Name);
        }

        public Unit(UnitType type, string name, string toName, string formula)
        {

            Name = name;
            Type = type;
            ToName = toName;
            Formula = formula;
        }
    }
}

