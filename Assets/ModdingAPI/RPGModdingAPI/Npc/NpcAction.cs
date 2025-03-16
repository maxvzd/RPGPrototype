namespace ModdingAPI.RPGModdingAPI.Npc
{
    public interface INpcAction
    { 
        IConsideration<IConsiderationContext>[] Considerations { get; }
        void Execute(INpcController npcController);
    }
}