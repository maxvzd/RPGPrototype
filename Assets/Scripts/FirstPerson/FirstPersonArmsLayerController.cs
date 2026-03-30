using System;
using Constants;
using UnityEngine;

namespace FirstPerson
{
    public class FirstPersonArmsLayerController : MonoBehaviour
    {
        [SerializeField] private GameObject rightArmMesh;
        [SerializeField] private GameObject leftArmMesh;

        public void MoveLeftArmToDefaultLayer()
        {
            leftArmMesh.layer = LayerMask.NameToLayer(LayerConstants.Default);
        }
        
        public void MoveRightArmToDefaultLayer()
        {
            rightArmMesh.layer = LayerMask.NameToLayer(LayerConstants.Default);
        }

        public void MoveLeftArmToFirstPersonLayer()
        {
            leftArmMesh.layer = LayerMask.NameToLayer(LayerConstants.VisibleInFirstPerson);
        }
        
        public void MoveRightArmToFirstPersonLayer()
        {
            rightArmMesh.layer = LayerMask.NameToLayer(LayerConstants.VisibleInFirstPerson);
        }
    }
}
