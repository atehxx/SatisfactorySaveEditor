using System;
using System.IO;

namespace SatisfactorySaveParser.Game.FactoryGame.Shared.Blueprint
{
    [TypePath("/Game/FactoryGame/-Shared/Blueprint/BP_RailroadSubsystem.BP_RailroadSubsystem_C")]
    [Serializable]
    public class BP_RailroadSubsystem_C : SaveEntity
    {
        public BP_RailroadSubsystem_C() { }

        public BP_RailroadSubsystem_C(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }

        public override void ParseClassSpecificData(long length, BinaryReader reader)
        {
            long newLen = length;
            // there are 4 bytes => int32 = 0 in my saves, probably some array similar as in BP_CircuitSubsystem_C.
            // TODO when got a save with rails.
            // for now lets take all here and not dive into base class anymore
            if (length > 0)
            {
                UnknownData = reader.ReadBytes((int)length);
                newLen = 0;
            }

            base.ParseClassSpecificData(newLen, reader);
        }

        public override void SerializeClassSpecificData(BinaryWriter writer)
        {
            base.SerializeClassSpecificData(writer);
        }
    }
}
