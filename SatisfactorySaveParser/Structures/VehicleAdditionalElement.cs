using SatisfactorySaveParser.PropertyTypes.Structs;
using System.IO;

namespace SatisfactorySaveParser.Structures
{
    public class VehicleAdditionalElement
    {
        public string PointName { get; set; }
        // 53 extra bytes
        public Vector Position { get; set; }
        public Quat Rotation { get; set; }
        public float Unk01 { get; set; } // 0 for freshly built and unused vehicle
        public float Unk02 { get; set; } // 0 for freshly built and unused vehicle
        public float Unk03 { get; set; } // 0 for freshly built and unused vehicle
        public float Unk04 { get; set; } // 0 for freshly built and unused vehicle
        public float Unk05 { get; set; } // 0 for freshly built and unused vehicle
        public float Unk06 { get; set; } // 0 for freshly built and unused vehicle
        public byte Unk07 { get; set; } // 1 for freshly built and unused vehicle, 0 for used vehicle

        public int SerializedLength => PointName.GetSerializedLength() + Position.SerializedLength + Rotation.SerializedLength + 25;

        public VehicleAdditionalElement(BinaryReader reader)
        {
            PointName = reader.ReadLengthPrefixedString();
            Position = new Vector(reader);
            Rotation = new Quat(reader);
            Unk01 = reader.ReadSingle();
            Unk02 = reader.ReadSingle();
            Unk03 = reader.ReadSingle();
            Unk04 = reader.ReadSingle();
            Unk05 = reader.ReadSingle();
            Unk06 = reader.ReadSingle();
            Unk07 = reader.ReadByte();
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteLengthPrefixedString(PointName);
            Position.Serialize(writer);
            Rotation.Serialize(writer);
            writer.Write(Unk01);
            writer.Write(Unk02);
            writer.Write(Unk03);
            writer.Write(Unk04);
            writer.Write(Unk05);
            writer.Write(Unk06);
            writer.Write(Unk07);
        }

        public override string ToString() => $"{PointName} {Position} {Rotation} {Unk01} {Unk02} {Unk03} {Unk04} {Unk05} {Unk06} {Unk07}";
    }
}
