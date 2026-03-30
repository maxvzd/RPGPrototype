using System;
using Constants;
using Items;
using Items.Equipment;
using Items.Equipment.Sheathing;
using Registries;
using UnityEngine;

namespace FirstPerson
{
    public class FirstPersonWeaponSheathing : MonoBehaviour
    {
        private EquipmentSlotManager _equipmentSlots;
        private FirstPersonCombatAnimationManager _animationManager;
        private FirstPersonArmsLayerController _armLayerController;
        private WeaponPositionManager  _positionManager;
        private FirstPersonRotateArms _firstPersonArmMovement;
        private EquippedMeshManager _equippedMeshManager;

        private void Start()
        {
            _equipmentSlots = GetComponent<EquipmentSlotManager>();
            _animationManager = GetComponent<FirstPersonCombatAnimationManager>();
            _armLayerController = GetComponent<FirstPersonArmsLayerController>();
            _positionManager = GetComponent<WeaponPositionManager>();
            _firstPersonArmMovement = GetComponent<FirstPersonRotateArms>();
            _equippedMeshManager = GetComponent<EquippedMeshManager>();
            
            var animationEventHandler = GetComponent<PlayerAnimationEventListener>();
            animationEventHandler.WeaponSheathed += AnimationWeaponSheathed;
            animationEventHandler.WeaponUnSheathed += AnimationWeaponUnSheathed;
            
            var sheathing = GetComponent<WeaponSheathing>();
            sheathing.WeaponSheathed += SheatheWeapon;
            sheathing.WeaponUnSheathed += UnsheatheWeapon;
            sheathing.OffhandSheathed += SheatheOffhand;
            sheathing.OffhandUnSheathed += UnsheatheOffhand;
        }

        private void SheatheWeapon(object sender, EventArgs e)
        {
            if (!_equipmentSlots.IsWeaponEquipped) return;
            _animationManager.MoveToSheatheState(FirstPersonAnimationLayers.RightArm);
        }

        private void SheatheOffhand(object sender, EventArgs e)
        {
            if (!_equipmentSlots.IsOffHandEquipped) return;
            _animationManager.MoveToSheatheState(FirstPersonAnimationLayers.LeftArm);
        }
        
        private void UnsheatheWeapon(object sender, EventArgs e)
        {
            if (!_equipmentSlots.IsWeaponEquipped) return;
            _animationManager.MoveToUnsheatheState(FirstPersonAnimationLayers.RightArm);
        }

        private void UnsheatheOffhand(object sender, EventArgs e)
        {
            if (!_equipmentSlots.IsOffHandEquipped) return;
            _animationManager.MoveToUnsheatheState(FirstPersonAnimationLayers.LeftArm);
        }
        
        private void AnimationWeaponSheathed(object sender, EventArgs e)
        {
            _positionManager.MoveItemsToSheathedSocket();
            _firstPersonArmMovement.ShouldRotateArmsWithCamera = false;
            if (_equipmentSlots.IsWeaponEquipped)
            {
                _armLayerController.MoveRightArmToDefaultLayer();
                SetLayerEquippedOnGameObject(ItemType.Weapon, LayerConstants.Default);
            }

            if (_equipmentSlots.IsOffHandEquipped)
            {
                _armLayerController.MoveLeftArmToDefaultLayer();
                SetLayerEquippedOnGameObject(ItemType.Offhand, LayerConstants.Default);
            }
        }
        
        private void AnimationWeaponUnSheathed(object sender, EventArgs e)
        {
            _positionManager.MoveItemsToWieldedSocket();
            _firstPersonArmMovement.ShouldRotateArmsWithCamera = true;
            
            if (_equipmentSlots.IsWeaponEquipped)
            {
                _armLayerController.MoveRightArmToFirstPersonLayer();
                SetLayerEquippedOnGameObject(ItemType.Weapon, LayerConstants.VisibleInFirstPerson);
            }

            if (_equipmentSlots.IsOffHandEquipped)
            {
                _armLayerController.MoveLeftArmToFirstPersonLayer();
                SetLayerEquippedOnGameObject(ItemType.Offhand, LayerConstants.VisibleInFirstPerson);
            }
        }

        private void SetLayerEquippedOnGameObject(ItemType itemType, string layerName)
        {
            var instanceId = _equipmentSlots.EquipmentSlots[itemType].GetFirstItem();
            var itemGameObject = _equippedMeshManager.EquippedGameObjects[instanceId];
            var layerMask = LayerMask.NameToLayer(layerName);
            SetLayerRecursively(itemGameObject, layerMask);
        }
        
        private static void SetLayerRecursively(GameObject obj, int layer)
        {
            obj.layer = layer;
            for (var i = 0; i < obj.transform.childCount; i++)
                SetLayerRecursively(obj.transform.GetChild(i).gameObject, layer);
        }
    }
}