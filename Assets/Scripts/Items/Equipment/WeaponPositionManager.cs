using System;
using System.Collections.Generic;
using System.Linq;
using Items.Equipment.Sheathing;
using UnityEngine;

namespace Items.Equipment
{
    public class WeaponPositionManager : MonoBehaviour
    {
        private PlayerAnimationEventListener _animationEventHandler;
        private Guid _equippedInstance;
        private Dictionary<ItemType, ISocket> _sockets;

        [SerializeField] private Transform rightHandSocket;
        [SerializeField] private Transform leftHandSocket;
        [SerializeField] private Transform sheathedSocket;

        public void Start()
        {
            _sockets = new Dictionary<ItemType, ISocket>
            {
                { ItemType.Weapon, new EmptySocket(rightHandSocket, sheathedSocket, ItemType.Weapon) },
                { ItemType.Offhand, new EmptySocket(leftHandSocket, sheathedSocket, ItemType.Offhand) }
            };
        }

        
        public void MoveItemsToWieldedSocket()
        {
            foreach (var value in _sockets)
            {
                value.Value.MoveToWieldedSocket();
            }
        }
        
        public void MoveItemsToSheathedSocket()
        {
            foreach (var socket in _sockets.Values)
            {
                socket.MovedToSheathedSocket();
            }
        }

        public void AddItemToSlot(Guid id, GameObject itemGameObject, IEnumerable<SheathedItemPositions> positions, ItemType slot)
        {
            var position = positions.FirstOrDefault(x => x.Type == slot);
            if (position is not null)
            {
                _sockets[slot] = _sockets[slot].AddItem(new WeaponPositionInfo(id, itemGameObject, position));
                _sockets[slot].MovedToSheathedSocket();
            }
        }

        public void RemoveItem(Guid id)
        {
            var socketsToRemove = _sockets.Values.Where(socket => socket.Contains(id)).ToList();
            foreach (var socket in socketsToRemove)
            {
                _sockets[socket.ForItemType] = socket.RemoveItem();
            }
        }

        private interface ISocket
        {
            bool IsEmpty { get; }
            Socket AddItem(WeaponPositionInfo weapon);
            EmptySocket RemoveItem();
            void MoveToWieldedSocket();
            void MovedToSheathedSocket();
            bool Contains(Guid id);
            ItemType ForItemType { get; }
        }

        private abstract class BaseSocket : ISocket
        {
            public ItemType ForItemType { get; }
            protected readonly Transform WieldedSocket;
            protected readonly Transform SheathedSocket;
            public virtual bool IsEmpty => true;

            protected BaseSocket(Transform wieldedSocket, Transform sheathedSocket, ItemType forItemType)
            {
                WieldedSocket = wieldedSocket;
                SheathedSocket = sheathedSocket;
                ForItemType = forItemType;
            }

            public Socket AddItem(WeaponPositionInfo weapon)
            {
                return new Socket(weapon, WieldedSocket, SheathedSocket, ForItemType);
            }

            public EmptySocket RemoveItem()
            {
                return new EmptySocket(WieldedSocket, SheathedSocket, ForItemType);
            }

            public virtual void MoveToWieldedSocket() { }
            public virtual void MovedToSheathedSocket() { }
            public virtual bool Contains(Guid id) => false;
        }
        
        private class EmptySocket : BaseSocket
        {
            public EmptySocket(Transform wieldedSocket, Transform sheathedSocket, ItemType forItemType) : base(wieldedSocket, sheathedSocket, forItemType) { }
        }

        private class Socket : BaseSocket
        {
            public override bool IsEmpty => false;
            private readonly WeaponPositionInfo _weaponPositionInfo;

            public Socket(WeaponPositionInfo weaponPositionInfo, Transform wieldedSocket, Transform sheathedSocket, ItemType forItemType) : base(wieldedSocket, sheathedSocket, forItemType)
            {
                _weaponPositionInfo = weaponPositionInfo;
            }

            public override void MoveToWieldedSocket()
            {
                SetItemPosition(
                    WieldedSocket, 
                    _weaponPositionInfo.SheathePositions.WieldedPosition, 
                    _weaponPositionInfo.SheathePositions.WieldedRotation, 
                    _weaponPositionInfo.GameObject);
            }

            public override void MovedToSheathedSocket()
            {
                SetItemPosition(
                    SheathedSocket, 
                    _weaponPositionInfo.SheathePositions.SheathedPosition, 
                    _weaponPositionInfo.SheathePositions.SheathedRotation, 
                    _weaponPositionInfo.GameObject);
            }

            public override bool Contains(Guid id)
            {
                return _weaponPositionInfo.Id == id;
            }

            private static void SetItemPosition(Transform socket, Vector3 pos, Vector3 rot, GameObject gameObject)
            {
                //gameObject.layer = LayerMask.NameToLayer(layerName);
                for (var i = 0; i < gameObject.transform.childCount; i++)
                {
                    var child = gameObject.transform.GetChild(i);
                    //child.gameObject.layer = LayerMask.NameToLayer(layerName);
                }
                
                gameObject.transform.SetParent(socket);
                gameObject.transform.SetLocalPositionAndRotation(pos, Quaternion.Euler(rot));
            }
        }

        private class WeaponPositionInfo
        {
            public Guid Id { get; }
            public GameObject GameObject { get; }
            public SheathedItemPositions SheathePositions { get; }

            public WeaponPositionInfo(Guid id, GameObject gameObject, SheathedItemPositions sheathePosition)
            {
                Id = id;
                GameObject = gameObject;
                SheathePositions = sheathePosition;
            }
        }
    }
}