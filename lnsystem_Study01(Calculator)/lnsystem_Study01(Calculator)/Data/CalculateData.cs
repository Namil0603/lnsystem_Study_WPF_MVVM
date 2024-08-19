using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lnsystem_Study01_Calculator_.Data
{
    public class CalculateData
    {
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public Operator Operator { get; set; }
        public double Result { get; set; }

    }

    public enum Operator
    {
        Plus,
        Minus,
        Multiply,
        Divide
    }
}
