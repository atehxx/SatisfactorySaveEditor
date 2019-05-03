using System.IO;

namespace SatisfactorySaveParser.PropertyTypes.Structs
{
    [System.Serializable]
    public class Rotator : Vector
    {
        public new string Type => "Rotator";

        public Rotator(BinaryReader reader) : base(reader)
        {
        }
    }
}
