using SatisfactorySaveParser.Structures;
using System;
using System.Collections.Generic;
using System.IO;

namespace SatisfactorySaveParser.Game.FactoryGame.Buildable.Vehicle
{
    [Serializable]
    public class Vehicle : SaveEntity
    {
        public List<VehicleAdditionalElement> VehicleAdditionalElements { get; } = new List<VehicleAdditionalElement>();

        public Vehicle() { }

        public Vehicle(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }

        public override void ParseClassSpecificData(long length, BinaryReader reader)
        {
            long newLength = length;
            if (length > 0)
            {
                int elementsCount = reader.ReadInt32();
                newLength -= 4;

                for (int i = 0; i < elementsCount; i++)
                {
                    VehicleAdditionalElement element = new VehicleAdditionalElement(reader);
                    newLength -= element.SerializedLength;

                    VehicleAdditionalElements.Add(element);
                }

            }

            base.ParseClassSpecificData(newLength, reader);
        }

        public override void SerializeClassSpecificData(BinaryWriter writer)
        {
            writer.Write(VehicleAdditionalElements.Count);
            foreach (var element in VehicleAdditionalElements)
            {
                element.SerializeData(writer);
            }

            base.SerializeClassSpecificData(writer);
        }
    }
}
