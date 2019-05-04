using SatisfactorySaveParser.Structures;
using System.IO;

namespace SatisfactorySaveParser.PropertyTypes.Structs
{
    [System.Serializable]
    public class Quat : Vector4, IStructData
    {
        public int SerializedLength => 16;
        public string Type => "Quat";

        public Quat(BinaryReader reader)
        {
            X = reader.ReadSingle();
            Y = reader.ReadSingle();
            Z = reader.ReadSingle();
            W = reader.ReadSingle();
        }

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
            writer.Write(W);
        }
    }
}
