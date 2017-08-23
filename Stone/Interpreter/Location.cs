namespace Stone.Interpreter
{
    public class Location
    {
        public Location(int nest, int index)
        {
            this.Nest = nest;
            this.Index = index;
        }

        public int Nest
        {
            get;
        }

        public int Index
        {
            get;
        }
    }
}