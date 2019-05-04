using NLog;
using System;
using System.IO;

namespace SatisfactorySaveParser
{
    /// <summary>
    ///     Class representing a single saved object in a Satisfactory save
    ///     Engine class: FObjectBaseSaveHeader
    /// </summary>
    [Serializable]
    public abstract class SaveObject
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();


        /// <summary>
        ///     Forward slash separated path of the script/prefab of this object.
        ///     Can be an empty string.
        /// </summary>
        public string TypePath { get; set; }

        /// <summary>
        ///     Root object (?) of this object
        ///     Often some form of "Persistent_Level", can be an empty string
        /// </summary>
        public string RootObject { get; set; }

        /// <summary>
        ///     Unique (?) name of this object
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// Numeric Id part of <c>InstanceName</c> (last number).
        /// <para>Used to find next valid number for new objects by iterating objects of this typepath</para>
        /// </summary>
        public int SequentialInstanceId
        {
            get
            {
                var parts = InstanceName.Split(new char[1] { '_' });
                if (parts.Length > 0)
                {
                    var lastPart = parts[parts.Length - 1];
                    int id = 0;
                    if (int.TryParse(lastPart, out id))
                    {
                        return id;
                    }
                }
                return 0;
            }
        }

        /// <summary>
        ///     Main serialized data of the object
        /// </summary>
        public SerializedFields DataFields { get; set; }

        /// <summary>
        /// Not yet understood data of this object
        /// </summary>
        public byte[] UnknownData { get; set; }

        public SaveObject(string typePath, string rootObject, string instanceName)
        {
            TypePath = typePath;
            RootObject = rootObject;
            InstanceName = instanceName;
        }

        protected SaveObject()
        {
        }

        public virtual void ParseHeader(BinaryReader reader)
        {
            //TypePath = reader.ReadLengthPrefixedString(); // parsed outside
            RootObject = reader.ReadLengthPrefixedString();
            InstanceName = reader.ReadLengthPrefixedString();
        }

        public virtual void SerializeHeader(BinaryWriter writer)
        {
            writer.WriteLengthPrefixedString(TypePath);
            writer.WriteLengthPrefixedString(RootObject);
            writer.WriteLengthPrefixedString(InstanceName);
        }

        public virtual void SerializeData(BinaryWriter writer)
        {
            DataFields.Serialize(writer);
            SerializeClassSpecificData(writer);
        }

        public virtual void ParseData(int length, BinaryReader reader)
        {
            long remainingBytes = 0;
            DataFields = SerializedFields.Parse(length, reader, out remainingBytes);
            ParseClassSpecificData(remainingBytes, reader);
        }

        public virtual void ParseClassSpecificData(long length, BinaryReader reader)
        {
            if (length > 0)
            {
                //log.Warn($"{length} bytes left after reading all serialized fields!");
                //log.Trace($"In object: {InstanceName}");
                UnknownData = reader.ReadBytes((int)length);
                //log.Trace(BitConverter.ToString(UnknownData).Replace("-", " "));
                //log.Trace(System.Text.Encoding.UTF8.GetString(UnknownData));
            }
        }

        public virtual void SerializeClassSpecificData(BinaryWriter writer)
        {
            if (UnknownData != null)
            {
                writer.Write(UnknownData);
            }

        }

        public override string ToString()
        {
            return TypePath;
        }
    }
}
