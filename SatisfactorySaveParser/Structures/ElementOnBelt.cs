using System;
using System.IO;

namespace SatisfactorySaveParser.Structures
{
    public class ElementOnBelt
    {
        public Int32 Unk1 { get; set; }
        public string ItemName { get; set; }
        public float Unk2 { get; set; } // probably part of vector
        public float Unk3 { get; set; } // probably part of vector
        public float Pos { get; set; } // probably part of vector

        public int SerializedLength => ItemName.GetSerializedLength() + 4 * 4;

        public ElementOnBelt(BinaryReader reader)
        {
            Unk1 = reader.ReadInt32();
            ItemName = reader.ReadLengthPrefixedString();
            Unk2 = reader.ReadSingle();
            Unk3 = reader.ReadSingle();
            Pos = reader.ReadSingle();
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.Write(Unk1);
            writer.WriteLengthPrefixedString(ItemName);
            writer.Write(Unk2);
            writer.Write(Unk3);
            writer.Write(Pos);
        }

        public override string ToString()
        {
            return $"{Convert.ToString(Unk1, 16)} {ItemName} {Unk2} {Unk3} {Pos}";
        }
    }
}
