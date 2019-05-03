using NLog;
using SatisfactorySaveParser.PropertyTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SatisfactorySaveParser
{
    [Serializable]
    public class SerializedFields : List<SerializedProperty>
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public void Serialize(BinaryWriter writer)
        {
            foreach (var field in this)
            {
                field.Serialize(writer);
            }

            writer.WriteLengthPrefixedString("None");

            writer.Write(0);
        }

        public static SerializedFields Parse(int length, BinaryReader reader, out long remainingBytes)
        {
            var start = reader.BaseStream.Position;
            var result = new SerializedFields();

            SerializedProperty prop;
            while ((prop = SerializedProperty.Parse(reader)) != null)
            {
                result.Add(prop);
            }

            var int1 = reader.ReadInt32();
            Trace.Assert(int1 == 0);

            remainingBytes = start + length - reader.BaseStream.Position;

            //if (remainingBytes == 4)
            ////if(result.Fields.Count > 0)
            //{
            //    var int2 = reader.ReadInt32();
            //}
            //else if (remainingBytes > 0 && result.Any(f => f is ArrayProperty && ((ArrayProperty)f).Type == StructProperty.TypeName))
            //{
            //    var unk = reader.ReadBytes((int)remainingBytes);
            //}
            //else if (remainingBytes > 4)
            //{
            //    var int2 = reader.ReadInt32();
            //    var str2 = reader.ReadLengthPrefixedString();
            //    var str3 = reader.ReadLengthPrefixedString();
            //}


            return result;
        }
    }
}
