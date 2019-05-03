using System;
using System.IO;

namespace SatisfactorySaveParser.Structures
{
    /// <summary>
    ///     Engine class: FObjectReferenceDisc
    /// </summary>
    [Serializable]
    public class ObjectReference : IObjectReference
    {
        public string LevelName { get; set; }
        public string PathName { get; set; }
        public SaveObject ReferencedObject { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObjectReference()
        {
        }

        public ObjectReference(BinaryReader reader)
        {
            LevelName = reader.ReadLengthPrefixedString();
            PathName = reader.ReadLengthPrefixedString();
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteLengthPrefixedString(LevelName);
            writer.WriteLengthPrefixedString(PathName);
        }

        public override string ToString()
        {
            return $"Level: {LevelName} Path: {PathName}";
        }
    }
}
