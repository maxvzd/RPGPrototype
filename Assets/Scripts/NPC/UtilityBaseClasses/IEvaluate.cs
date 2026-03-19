using System;
using NPC.Context;

namespace NPC.UtilityBaseClasses
{
    public interface IEvaluate
    {
        float Evaluate(Guid id, NpcContext context);
    }
}