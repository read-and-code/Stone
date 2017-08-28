namespace Stone.Interpreter
{
    public class MemberSymbolTable : SymbolTable
    {
        public const int EntityTypeMethod = -1;

        public const int EntityTypeField = -2;

        public MemberSymbolTable(SymbolTable outerSymbolTable, int memberType)
            : base(outerSymbolTable)
        {
            this.MemberType = memberType;
        }

        public int MemberType
        {
            get;
        }

        public override Location GetLocation(string key, int nest)
        {
            int index = this.FindIndex(key);

            if (index == -1)
            {
                if (this.OuterSymbolTable == null)
                {
                    return null;
                }
                else
                {
                    return this.OuterSymbolTable.GetLocation(key, nest);
                }
            }
            else
            {
                return new Location(this.MemberType, index);
            }
        }

        public override Location Put(string key)
        {
            Location location = this.GetLocation(key, 0);

            if (location == null)
            {
                return new Location(this.MemberType, this.Add(key));
            }
            else
            {
                return location;
            }
        }
    }
}