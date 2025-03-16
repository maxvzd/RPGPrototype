namespace ModdingAPI.RPGModdingAPI.Npc
{
    public interface IConsiderationManager
    {
        IConsideration<T> GetConsideration<T>() where T : IConsiderationContext;
    }
}