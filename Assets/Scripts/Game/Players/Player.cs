using System;
using System.Collections;
using Game.Misc;
using Game.Scriptable;
using Game.Shared;
using UnityEngine;

namespace Game.Players
{
    public class Player : MovableBase, ITeleportable
    {
        public float RotationSpeed;

        [SerializeField]
        private AudioClip _deathSound = null;

        private bool _isUndestructable = true;

        [SerializeField]
        private Weapon[] _weapons = null;

        #region Events

        protected virtual void FireIndestructableStateChanged(bool state)
        {
            Action<bool> handler = IndestructableStateChanged;
            if (handler != null)
            {
                handler(state);
            }
        }

        public event Action<bool> IndestructableStateChanged;

        #endregion

        public Field Field { get; set; }

        public Weapon[] Weapons
        {
            get
            {
                return _weapons;
            }
        }

        public bool IsUndestructable
        {
            get
            {
                return _isUndestructable;
            }
            set
            {
                _isUndestructable = value;
                FireIndestructableStateChanged(value);
            }
        }

        #region ITeleportable Members

        public bool WasTeleported { get; set; }

        public override void SelfDestroy()
        {
            SoundManager.PlaySound(_deathSound, 0.5f);

            if (Field.Player == this)
            {
                Field.Player = null;
            }

            base.SelfDestroy();
        }

        #endregion

        public void Shoot()
        {
            foreach (var weapon in Weapons)
            {
                var bullet = weapon.Shoot();
                if (bullet)
                {
                    Field.AddMovable(bullet);
                }
            }
        }

        public void Start()
        {
            if (Weapons == null || Weapons.Length == 0)
            {
                Debug.LogError("You didn't add any weapon");
            }
        }

        void OnEnable()
        {
            IsUndestructable = true;
            StartCoroutine(TurnOffUndestructable(Instance.GameLogicParameters.UndestructableTime));
        }

        private IEnumerator TurnOffUndestructable(float undestructableTime)
        {
            yield return new WaitForSeconds(undestructableTime);
            IsUndestructable = false;
        }

        public void Rotate(float timePassed)
        {
            transform.Rotate(new Vector3(0, 0, timePassed * RotationSpeed));
        }

        public void Rotate(Quaternion rotation, float timePassed)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, timePassed * 5);
        }

        public void Reload()
        {
            foreach (var weapon in Weapons)
            {
                weapon.Reload();
            }
        }
    }
}