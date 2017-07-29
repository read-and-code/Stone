namespace Stone.Parsers
{
    public class Precedence
    {
        public Precedence(int value, bool leftAssociative)
        {
            this.Value = value;
            this.LeftAssociative = leftAssociative;
        }

        public int Value { get; set; }

        public bool LeftAssociative { get; set; }
    }
}