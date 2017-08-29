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

        public override EntityLocation GetEntityLocation(string key, int nest)
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
                    return this.OuterSymbolTable.GetEntityLocation(key, nest);
                }
            }
            else
            {
                return new EntityLocation(this.MemberType, index);
            }
        }

        public override EntityLocation Put(string key)
        {
            EntityLocation entityLocation = this.GetEntityLocation(key, 0);

            if (entityLocation == null)
            {
                return new EntityLocation(this.MemberType, this.Add(key));
            }
            else
            {
                return entityLocation;
            }
        }
    }
}