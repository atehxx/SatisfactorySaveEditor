using SatisfactorySaveParser.Structures;
using System;
using System.Collections.Generic;
using System.IO;

namespace SatisfactorySaveParser.Game.FactoryGame.Shared.Blueprint
{
    [TypePath("/Game/FactoryGame/-Shared/Blueprint/BP_CircuitSubsystem.BP_CircuitSubsystem_C")]
    [Serializable]
    public class BP_CircuitSubsystem_C : SaveEntity
    {
        public Dictionary<int, ObjectReference> PowerCircuits { get; } = new Dictionary<int, ObjectReference>();

        public BP_CircuitSubsystem_C() { }
        public BP_CircuitSubsystem_C(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }

        public override void ParseClassSpecificData(long length, BinaryReader reader)
        {
            var newLength = length;
            if (length >= 4)
            {
                int elementsCount = reader.ReadInt32();
                newLength -= 4;

                for (int i = 0; i < elementsCount; i++)
                {
                    int circuitId = reader.ReadInt32();
                    newLength -= 4;
                    ObjectReference circuitObject = new ObjectReference(reader);
                    newLength -= circuitObject.SerializedLength;

                    PowerCircuits.Add(circuitId, circuitObject);
                }
            }

            base.ParseClassSpecificData(newLength, reader);
        }

        public override void SerializeClassSpecificData(BinaryWriter writer)
        {
            writer.Write(PowerCircuits.Count);
            foreach (var powerCircuit in PowerCircuits)
            {
                writer.Write(powerCircuit.Key);
                powerCircuit.Value.SerializeData(writer);
            }

            base.SerializeClassSpecificData(writer);
        }
    }
}
