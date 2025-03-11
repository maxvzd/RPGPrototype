namespace DataPersistence.SerializableClasses.OnObject
{
    public interface ISerializableGameObject<out T> where T : ISerializable
    {
        T GetSerializable();
    }
}