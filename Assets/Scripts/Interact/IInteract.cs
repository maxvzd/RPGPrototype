using System;

namespace Interact
{
    public interface IInteract
    {
        void Interact(IInteractionContext context);
        
        Type GetInteractionType();
    }
    
    public interface IInteract<in T> : IInteract where T : IInteractionContext
    {
        //void Interact(T interactionContext);
    }
}