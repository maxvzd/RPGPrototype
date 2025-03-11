using System.Collections.Generic;
using DataPersistence.SerializableClasses;

namespace DataPersistence
{
    public interface IDataLoader
    {
        List<SerializablePerson> LoadPeople();
    }
}