namespace Stone.Interpreter
{
    public class EntityLocation
    {
        public EntityLocation(int nest, int index)
        {
            this.Nest = nest;
            this.Index = index;
        }

        public int Nest
        {
            get;
            set;
        }

        public int Index
        {
            get;
        }
    }
}