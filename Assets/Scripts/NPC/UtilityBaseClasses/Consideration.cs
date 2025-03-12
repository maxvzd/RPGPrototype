using System;

namespace NPC.UtilityBaseClasses
{
    [Serializable]
    public abstract class Consideration<T> : ConsiderationBase where T : IConsiderationContext
    {
        public override float Score(IConsiderationContext context)
        {
            return Score((T)context);
        }

        public abstract float Score(T context);
    }
}