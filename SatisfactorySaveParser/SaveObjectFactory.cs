using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SatisfactorySaveParser
{
    public class SaveObjectFactory
    {
        private static Dictionary<string, Type> _knownEntityTypes;
        private static Dictionary<string, Type> _knownComponentsTypes;
        public static Dictionary<string, Type> KnownEntityTypes
        {
            get
            {
                if (null == _knownEntityTypes)
                {
                    PopulateKnownEntityTypes();
                }
                return _knownEntityTypes;
            }
        }
        public static Dictionary<string, Type> KnownComponentsTypes
        {
            get
            {
                if (null == _knownComponentsTypes)
                {
                    PopulateKnownComponentTypes();
                }
                return _knownComponentsTypes;
            }
        }

        private static void PopulateKnownEntityTypes()
        {
            if (null == _knownEntityTypes)
            {
                _knownEntityTypes = new Dictionary<string, Type>();
                //_knownEntityTypes.Add("base", typeof(SaveEntity));
                Type entityType = typeof(SaveEntity);
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(entityType)))
                {
                    TypePathAttribute attr = type.GetCustomAttribute<TypePathAttribute>(false);
                    if (null != attr)
                    {
                        _knownEntityTypes.Add(attr.TypePath, type);
                    }
                }
            }
        }
        private static void PopulateKnownComponentTypes()
        {
            if (null == _knownComponentsTypes)
            {
                _knownComponentsTypes = new Dictionary<string, Type>();
                //_knownComponentsTypes.Add("base", typeof(SaveComponent));
                Type componentType = typeof(SaveComponent);
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(componentType)))
                {
                    TypePathAttribute attr = type.GetCustomAttribute<TypePathAttribute>(false);
                    if (null != attr)
                    {
                        _knownComponentsTypes.Add(attr.TypePath, type);
                    }
                }
            }
        }

        public static SaveObject CreateObjectFromType(int typeId, string typePath)
        {
            Type type = null;
            switch (typeId)
            {
                case SaveComponent.TypeID:
                {
                    KnownComponentsTypes.TryGetValue(typePath, out type);
                    type = type ?? typeof(SaveComponent);
                }
                break;
                case SaveEntity.TypeID:
                {
                    KnownEntityTypes.TryGetValue(typePath, out type);
                    type = type ?? typeof(SaveEntity);
                }
                break;
            }
            if (null != type)
            {
                try
                {
                    SaveObject so = (SaveObject)Activator.CreateInstance(type);
                    return so;
                }
                catch
                {
                }
            }
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TypePathAttribute : Attribute
    {
        private readonly string _typePath;
        public string TypePath => _typePath;

        public TypePathAttribute(string typePath)
        {
            _typePath = typePath;
        }
    }
}
