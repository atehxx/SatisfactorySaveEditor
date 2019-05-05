using SatisfactorySaveParser.Structures;
using System;
using System.IO;

namespace SatisfactorySaveParser.Game.FactoryGame.Buildable.Factory.PowerLine
{
    [TypePath("/Game/FactoryGame/Buildable/Factory/PowerLine/Build_PowerLine.Build_PowerLine_C")]
    [Serializable]
    public class Build_PowerLine_C : SaveEntity
    {
        public ObjectReference WireSource { get; set; }
        public ObjectReference WireDestination { get; set; }

        public Build_PowerLine_C() : base() { }

        public Build_PowerLine_C(string typePath, string rootObject, string instanceName) : base(typePath, rootObject, instanceName) { }

        public override void ParseClassSpecificData(long length, BinaryReader reader)
        {
            var newLen = length;
            WireSource = new ObjectReference(reader);
            newLen -= WireSource.LevelName.GetSerializedLength();
            newLen -= WireSource.PathName.GetSerializedLength();
            WireDestination = new ObjectReference(reader);
            newLen -= WireDestination.LevelName.GetSerializedLength();
            newLen -= WireDestination.PathName.GetSerializedLength();

            base.ParseClassSpecificData(newLen, reader);
        }

        public override void SerializeClassSpecificData(BinaryWriter writer)
        {
            if (null != WireSource && null != WireDestination)
            {
                WireSource.SerializeData(writer);
                WireDestination.SerializeData(writer);
            }
            base.SerializeClassSpecificData(writer);
        }
    }
}
