namespace Interact
{
    public interface IInteract
    {
        void Interact(IInteractionContext context);
        string IconName { get; }
    }
    
    public interface IInteract<out T> : IInteract where T : IContextBuilder
    {
        T GetInteractionContext();
    }
}