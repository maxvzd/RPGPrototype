using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPC.Senses
{
    public class SightDetection : MonoBehaviour
    {
        [SerializeField] private float radius;
        [SerializeField, Range(0, 360)] private float fieldOfView;
        [SerializeField] private Transform eyes;
        
        public float Radius => radius;
        public float FieldOfView => fieldOfView;
        public IReadOnlyDictionary<Guid, Detectable> VisibleEntities => _visibleEntities;
        public IReadOnlyList<Transform> VisiblePoints => _visiblePoints;
        public Vector3 EyePos => eyes.position;
        
        private readonly Dictionary<Guid, Detectable> _entitiesInRange = new();
        private readonly Dictionary<Guid, Detectable> _visibleEntities = new();
        private readonly List<Transform> _visiblePoints = new();
        
        private HashSet<Guid> _recentlySeenEntities = new();
        
        private Guid Id { get; set; }

        public void Start()
        {
            Id = GetComponent<Entity>().Id;
            StartCoroutine(CanSeeEntities());
        }

        private IEnumerator CanSeeEntities()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                _visibleEntities.Clear();
                _visiblePoints.Clear();
                foreach (var spottedEntity in _entitiesInRange.Values.Where(CanSeeEntity))
                {
                    if (!_recentlySeenEntities.Contains(spottedEntity.Id))
                    {
                        var entityInfo = Entities.List[Id];
                        if (entityInfo is NpcEntity npc)
                        {
                            npc.NpcInfo.Brain.SpottedEntity(spottedEntity.Id);
                        }
                    }
                    _visibleEntities.TryAdd(spottedEntity.Id, spottedEntity);
                    _recentlySeenEntities.Add(spottedEntity.Id);
                }
            }
        }

        private bool CanSeeEntity(Detectable entity)
        {
            var directionToTarget =  (entity.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) > fieldOfView / 2) return false;
            foreach (var detectablePoint in entity.DetectablePoints)
            {
                var directionToPoint =  (detectablePoint.position - eyes.transform.position).normalized;
                var didRaycastHit = Physics.Raycast(eyes.transform.position, directionToPoint, out var hit);
                if (didRaycastHit && hit.transform.gameObject == entity.gameObject)
                {
                    _visiblePoints.Add(detectablePoint.transform);
                    return true;
                }
            }
            return false;
        }

        public void AddNearbyEntity(Detectable detectable)
        {
            _entitiesInRange.Add(detectable.Id, detectable);
        }

        public void RemoveNearbyEntity(Detectable detectable)
        {
            _entitiesInRange.Remove(detectable.Id);
        }
    }
}