using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FirstPerson
{
    public class RotateArms : MonoBehaviour
    {
        [SerializeField] private Transform objectToMatch;
        [SerializeField] private List<AnimationOffset> offsets;
        [SerializeField] private float angleToStopTurningArms;
        private Animator _animator;
        public bool ShouldRotateArmsWithCamera { get; set; }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
    
        private void OnAnimatorIK(int layerIndex)
        {
            if (!ShouldRotateArmsWithCamera) return;
        
            var bone = layerIndex switch
            {
                1 => HumanBodyBones.RightShoulder,
                2 => HumanBodyBones.LeftShoulder,
                _ => new HumanBodyBones()
            };

            if (bone == new HumanBodyBones()) return;
        
            var currentRotation =  _animator.GetBoneTransform(bone).localEulerAngles;
            var targetRotation = objectToMatch.localEulerAngles;

            //Just limit the rotation??
            if (targetRotation.x > angleToStopTurningArms && targetRotation.x < 180)
            {
                targetRotation.x = angleToStopTurningArms;
            }
            
            currentRotation.y -= targetRotation.x;
            
            //Blend between two targets
            // var blendFactor = 1f;
            // if (targetRotation.x < 180)
            // {
            //     blendFactor = Mathf.Clamp01((angleToStopTurningArms - targetRotation.x) / angleToStopTurningArms);
            // }
            //currentRotation.y = Mathf.Lerp(currentRotation.y, currentRotation.y - targetRotation.x, blendFactor);

            var offset = offsets.First(x => x.Bone == bone).Offset;
            _animator.SetBoneLocalRotation(bone, Quaternion.Euler(currentRotation + offset));
        }
    
        [Serializable]
        private struct AnimationOffset
        {
            [SerializeField] private HumanBodyBones bone;
            [SerializeField] private Vector3 offset;
        
            public readonly HumanBodyBones Bone => bone;
            public readonly Vector3 Offset => offset;

            public AnimationOffset(HumanBodyBones bone, Vector3 offset)
            {
                this.bone = bone;
                this.offset = offset;
            }
        }
    }
}

