namespace Stone.Parsers
{
    public class Precedence
    {
        public Precedence(int value, bool isLeftAssociative)
        {
            this.Value = value;
            this.IsLeftAssociative = isLeftAssociative;
        }

        public int Value { get; set; }

        public bool IsLeftAssociative { get; set; }
    }
}