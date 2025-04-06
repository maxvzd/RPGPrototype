using Interact.Contexts;
using Items;

namespace Interact.ContextBuilders
{
    public class PickupContextBuilder : IContextBuilder
    {
        public PickupContext Build(Inventory inventory)
        {
            return new PickupContext(inventory);
        }
    }
}