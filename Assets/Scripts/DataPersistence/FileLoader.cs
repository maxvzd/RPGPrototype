using System.Collections.Generic;
using DataPersistence.SerializableClasses;

namespace DataPersistence
{
    public class FileLoader : IDataLoader
    {
        public List<SerializablePerson> LoadPeople()
        {
            return new List<SerializablePerson>();
        }
    }
}
