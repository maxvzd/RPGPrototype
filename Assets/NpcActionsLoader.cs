using System;
using System.Collections.Generic;
using System.Linq;
using NPC.UtilityBaseClasses;
using Unity.VisualScripting;
using UnityEngine;

public class NpcActionsLoader : MonoBehaviour
{
    public static NpcActionsLoader Instance { get; private set; }
    public IReadOnlyDictionary<string, NpcAction> Actions => _actions;
    
    private readonly Dictionary<string, NpcAction> _actions = new();
    
    private void Awake()
    {
        LoadScriptableObjects();
        
        if (Instance is null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception($"More than one instance of {nameof(NpcActionsLoader)} detected.");
        }
    }

    private void LoadScriptableObjects()
    {
        var actions = Resources.LoadAll<NpcAction>("Actions");

        foreach (var action in actions)
        {
            _actions.Add(action.ActionId, action);
        }
    }
}
