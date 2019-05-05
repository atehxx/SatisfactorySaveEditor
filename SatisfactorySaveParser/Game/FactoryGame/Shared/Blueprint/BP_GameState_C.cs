using SatisfactorySaveParser.Structures;
using System;
using System.Collections.Generic;
using System.IO;

namespace SatisfactorySaveParser.Game.FactoryGame.Shared.Blueprint
{
    [TypePath("/Game/FactoryGame/-Shared/Blueprint/BP_GameState.BP_GameState_C")]
    [Serializable]
    public class BP_GameState_C : SaveEntity
    {
        public List<ObjectReference> States = new List<ObjectReference>();

        public BP_GameState_C() { }

        public BP_GameState_C(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }

        public override void ParseClassSpecificData(long length, BinaryReader reader)
        {
            var newLength = length;
            if (length >= 4)
            {
                int elementsCount = reader.ReadInt32();
                newLength -= 4;

                for (int i = 0; i < elementsCount; i++)
                {
                    ObjectReference objectReference = new ObjectReference(reader);
                    newLength -= objectReference.SerializedLength;

                    States.Add(objectReference);
                }
            }

            base.ParseClassSpecificData(newLength, reader);
        }

        public override void SerializeClassSpecificData(BinaryWriter writer)
        {
            writer.Write(States.Count);
            foreach (var objectReference in States)
            {
                objectReference.SerializeData(writer);
            }

            base.SerializeClassSpecificData(writer);
        }
    }
}
