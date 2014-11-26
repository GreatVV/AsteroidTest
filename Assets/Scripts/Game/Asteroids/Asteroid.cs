using Game.Shared;
using UnityEngine;

namespace Game.Asteroids
{
    public class Asteroid : MovableBase, ITeleportable
    {
        public int TimeToDivide;
        private Vector3 _speed;

        [SerializeField]
        private bool usePhysics = false;

  

        #region ITeleportable Members

        public bool WasTeleported { get; set; }

        public override void Move(float timePassed)
        {
            if (!usePhysics)
            {
                base.Move(timePassed);
            }
        }

        public override Vector3 Speed
        {
            get
            {
                if (!usePhysics)
                {
                    return _speed;
                }
                return rigidbody2D.velocity;
            }
            set
            {
                if (!usePhysics)
                {
                    _speed = value;
                    return;
                }
                rigidbody2D.velocity = value;
            }
        }

        #endregion
    }
}