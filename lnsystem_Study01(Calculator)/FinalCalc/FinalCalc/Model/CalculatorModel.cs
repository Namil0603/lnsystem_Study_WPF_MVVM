namespace FinalCalc.Model
{
    public class CalculatorModel
    {
        public double FirstNumber { get; set; }
        public double SecondNumber { get; set; }
        public OperatorType Operator { get; set; }
        public double Result { get; set; }
    }
    public enum OperatorType
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
}