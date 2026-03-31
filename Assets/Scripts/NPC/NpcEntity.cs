using System;
using System.Collections;
using System.Linq;
using DataPersistence.Database.Models;
using Items;
using Items.Equipment;
using NPC.ScriptableObjectContexts;
using NPC.UtilityBaseClasses;
using UnityEngine;
using UnityEngine.AI;

namespace NPC
{
    public class NpcEntity : Entity
    {
        [SerializeField] private GoalAndContext[] goals = Array.Empty<GoalAndContext>();

        public NpcInfo NpcInfo { get; private set; }

        public void Initialise(DatabaseNpc npcState)
        {
            Id = Guid.Parse(npcState.Id);
            var navMeshAgent = GetComponent<NavMeshAgent>();
            var socialStats = GetComponent<SocialStats>();
            
            var state = new NpcState(npcState, navMeshAgent, socialStats);
            var controller = new NpcController(navMeshAgent, state);

            var goalsAndContexts = goals.Select(x => new UtilityBrain.GoalInfo(x.Goal, x.Context.Get(state))); 
            var brain = new UtilityBrain(Id, goalsAndContexts);
            NpcInfo = NpcInfo.Create(this, state, brain, controller);
            EntitiesRegistry.Register(this); 
        }

        public void Start()
        {
            //Temp code for equipping sword
            var inventory = GetComponent<Inventory>();
            var equipment = GetComponent<EquipmentSlotManager>();
            var item = inventory.Items.Values.FirstOrDefault();
            equipment.ToggleItemEquipped(item);
            //
            
            NpcInfo.Brain.ExecuteCoroutine += ExecuteCoroutine;
            NpcInfo.Brain.Start();
        }

        private void ExecuteCoroutine(object sender, IEnumerator e)
        {
            StartCoroutine(e);
        }

        
        [Serializable]
        private class GoalAndContext
        {
            [SerializeField] private UtilityGoal goal;
            [SerializeField] private ScriptableObjectContext context;
            
            public UtilityGoal Goal => goal;
            public ScriptableObjectContext Context => context;
        }
    }
}