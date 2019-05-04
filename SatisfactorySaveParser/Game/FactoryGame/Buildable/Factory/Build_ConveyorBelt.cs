using SatisfactorySaveParser.Structures;
using System.Collections.Generic;
using System.IO;

namespace SatisfactorySaveParser.Game.FactoryGame.Buildable.Factory
{
    public class Build_ConveyorBelt : SaveEntity
    {
        public List<ElementOnBelt> ElementsOnBelt { get; } = new List<ElementOnBelt>();

        public Build_ConveyorBelt() { }
        public Build_ConveyorBelt(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }

        public override void ParseClassSpecificData(long length, BinaryReader reader)
        {
            var newLength = length;
            if (length >= 4)
            {
                int elementsCount = reader.ReadInt32();
                newLength -= 4;

                for (int i = 0; i < elementsCount; i++)
                {
                    ElementOnBelt element = new ElementOnBelt(reader);
                    newLength -= element.SerializedLength;

                    ElementsOnBelt.Add(element);
                }
            }
            System.Diagnostics.Trace.Write("X");
            base.ParseClassSpecificData(newLength, reader);
        }

        public override void SerializeClassSpecificData(BinaryWriter writer)
        {
            writer.Write(ElementsOnBelt.Count);
            foreach (var element in ElementsOnBelt)
            {
                element.SerializeData(writer);
            }

            base.SerializeClassSpecificData(writer);
        }
    }
}
