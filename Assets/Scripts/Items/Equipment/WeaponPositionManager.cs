using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using FirstPerson;
using Items.Equipment.Sheathing;
using PlayerMovement;
using UnityEngine;

namespace Items.Equipment
{
    public class WeaponPositionManager : MonoBehaviour
    {
        public bool IsWeaponSheathed => _isWeaponSheathed;

        private Animator _animator;
        private bool _isWeaponSheathed = true;
        private PlayerAnimationEventListener _animationEventHandler;
        private bool IsWeaponEquipped => _weaponPositions is not null && _weaponPositions.Any(x => !x.Value.IsEmpty);
        private Guid _equippedInstance;
        private IReadOnlyDictionary<ItemType, SocketMap> _weaponPositions;

        [SerializeField] private Transform rightHandSocket;
        [SerializeField] private Transform leftHandSocket;
        [SerializeField] private Transform sheathedSocket;
        private RotateArms _armRotator;
        private FirstPersonCameraSwap _armSwap;

        public void Start()
        {
            _animator = GetComponent<Animator>();
            _animationEventHandler = GetComponent<PlayerAnimationEventListener>();
            _animationEventHandler.WeaponSheathed += WeaponSheathed;
            _animationEventHandler.WeaponUnSheathed += WeaponSheathed;
            _animator.SetBool(AnimatorConstants.WeaponSheathed, _isWeaponSheathed);
            
            _armSwap = GetComponent<FirstPersonCameraSwap>();
            
            _weaponPositions = new Dictionary<ItemType, SocketMap>
            {
                { ItemType.Weapon, new SocketMap(Guid.Empty, rightHandSocket, sheathedSocket, null, null) },
                { ItemType.Offhand, new SocketMap(Guid.Empty, leftHandSocket, sheathedSocket, null, null) }
            };

            _armRotator = GetComponent<RotateArms>();
            
            UpdateWeaponPosition();
        }

        private void WeaponSheathed(object sender, EventArgs e)
        {
            UpdateWeaponPosition();
        }

        public void SheatheWeapon()
        {
            if (!IsWeaponEquipped) return;
            
            _isWeaponSheathed = !_isWeaponSheathed;
            _armRotator.ShouldRotateArmsWithCamera = !_isWeaponSheathed;
            _armSwap.SwitchArms();
            
            _animator.SetBool(AnimatorConstants.WeaponSheathed, _isWeaponSheathed);
        }

        public void EquipWeapon(Guid instanceId, GameObject weaponTransform, IEnumerable<SheathedItemPositions> positions, ItemType slot)
        {
            _isWeaponSheathed = true;

            var position = positions.FirstOrDefault(x => x.Type == slot);
            if (position is not null)
            {
                _weaponPositions[slot].EquipItem(instanceId, weaponTransform, position);
            }

            UpdateWeaponPosition();
        }

        public void UnEquipWeapon(Guid id)
        {
            foreach (var weaponPosition in _weaponPositions)
            {
                if (!weaponPosition.Value.IsItem(id)) continue;

                weaponPosition.Value.UnEquipItem();
                return;
            }
        }

        private void UpdateWeaponPosition()
        {
            if (!IsWeaponEquipped) return;
            if (!_isWeaponSheathed)
            {
                PlayerTurner.BodyShouldFollowCameraRegister();
                foreach (var value in _weaponPositions)
                {
                    value.Value.UnsheatheItem();
                }
            }
            else
            {
                PlayerTurner.BodyShouldFollowCameraUnRegister();
                foreach (var value in _weaponPositions)
                {
                    value.Value.SheatheItem();
                }
            }
        }

        private class SocketMap
        {
            public bool IsEmpty { get; private set; } = true;

            private Guid _instanceId;
            private readonly Transform _wieldedSocket;
            private readonly Transform _sheathedSocket;
            private GameObject _item;
            private SheathedItemPositions _sheathePosition;

            public SocketMap(Guid instanceId, Transform wieldedSocket, Transform sheathedSocket, GameObject item, SheathedItemPositions sheathePosition)
            {
                _instanceId = instanceId;
                _wieldedSocket = wieldedSocket;
                _sheathedSocket = sheathedSocket;
                _item = item;
                _sheathePosition = sheathePosition;
            }

            public void EquipItem(Guid id, GameObject item, SheathedItemPositions sheathePosition)
            {
                IsEmpty = false;
                _instanceId = id;
                _sheathePosition = sheathePosition;
                _item = item;
            }

            public void UnEquipItem()
            {
                IsEmpty = true;
                _instanceId = Guid.Empty;
                _sheathePosition = null;
                _item = null;
            }

            public void UnsheatheItem()
            {
                if (IsEmpty) return;
                SetItemPosition(LayerConstants.VisibleInFirstPerson, _wieldedSocket, _sheathePosition.WieldedPosition, _sheathePosition.WieldedRotation);
            }

            public void SheatheItem()
            {
                if (IsEmpty) return;
                SetItemPosition(LayerConstants.Default, _sheathedSocket, _sheathePosition.SheathedPosition, _sheathePosition.SheathedRotation);
            }

            private void SetItemPosition(string layerName, Transform socket, Vector3 pos, Vector3 rot)
            {
                _item.layer = LayerMask.NameToLayer(layerName);
                for (var i = 0; i < _item.transform.childCount; i++)
                {
                    var child = _item.transform.GetChild(i);
                    child.gameObject.layer = LayerMask.NameToLayer(layerName);
                }

                _item.transform.SetParent(socket);
                _item.transform.SetLocalPositionAndRotation(pos, Quaternion.Euler(rot));
            }

            public bool IsItem(Guid id)
            {
                return id == _instanceId;
            }
        }
    }
}