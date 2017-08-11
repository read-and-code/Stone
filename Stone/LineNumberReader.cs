using System.IO;

namespace Stone
{
    public class LineNumberReader : StreamReader
    {
        public LineNumberReader(Stream stream)
            : base(stream)
        {
        }

        public int LineNumber
        {
            get;
            private set;
        }

        public override string ReadLine()
        {
            this.LineNumber++;

            return base.ReadLine();
        }
    }
}