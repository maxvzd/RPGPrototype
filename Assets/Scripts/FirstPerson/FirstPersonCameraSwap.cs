using System;
using Constants;
using UnityEngine;

namespace FirstPerson
{
    public class FirstPersonCameraSwap : MonoBehaviour
    {
        [SerializeField] private GameObject rightArmMesh;
        [SerializeField] private GameObject leftArmMesh;

        private bool _weaponIsSheathed = true;
        private PlayerAnimationEventListener _animationEventHandler;

        public void Start()
        {
            _animationEventHandler = GetComponent<PlayerAnimationEventListener>();
            _animationEventHandler.WeaponSheathed += WeaponSheathed;
        }

        private void WeaponSheathed(object sender, EventArgs e)
        {
            rightArmMesh.layer = LayerMask.NameToLayer(LayerConstants.Default);
            leftArmMesh.layer = LayerMask.NameToLayer(LayerConstants.Default);
            _weaponIsSheathed = true;
        }

        public void SwitchArms()
        {
            if (!_weaponIsSheathed) return;
        
            rightArmMesh.layer = LayerMask.NameToLayer(LayerConstants.VisibleInFirstPerson);
            leftArmMesh.layer = LayerMask.NameToLayer(LayerConstants.VisibleInFirstPerson);
            _weaponIsSheathed = false;
        }
    }
}
