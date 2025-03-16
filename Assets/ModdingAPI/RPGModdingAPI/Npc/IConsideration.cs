namespace ModdingAPI.RPGModdingAPI.Npc
{
    public interface IConsideration<in T> where T : IConsiderationContext
    {
        public abstract float Score(T context);
    }
}