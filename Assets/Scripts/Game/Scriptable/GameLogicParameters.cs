using UnityEngine;

namespace Game.Scriptable
{
    public class GameLogicParameters : ScriptableObject
    {

        private static GameLogicParameters _instance;
        [SerializeField]
        private float _minUfoInterval = 30;
        [SerializeField]
        private float _maxUfoInterval = 60;

        [SerializeField]
        private int _defaultNumberOfNewAsteroids = 4;

        [SerializeField]
        private float _defaultPlayerSpeed = 3;

        [SerializeField]
        private float _playerRotateSpeed = 360;

        [SerializeField]
        private int _pointsTillUfo = 100;

        [SerializeField]
        private int _startNumberOfLifes = 3;

        [SerializeField]
        private float _undestructableTime = 3f;

       

        public float PlayerRotateSpeed
        {
            get
            {
                return _playerRotateSpeed;
            }
        }

        public int DefaultNumberOfNewAsteroids
        {
            get
            {
                return _defaultNumberOfNewAsteroids;
            }
        }

        public float DefaultPlayerSpeed
        {
            get
            {
                return _defaultPlayerSpeed;
            }
        }

        public int StartNumberOfLifes
        {
            get
            {
                return _startNumberOfLifes;
            }
        }

        public float UndestructableTime
        {
            get
            {
                return _undestructableTime;
            }
        }

        public int PointsTillUfo
        {
            get
            {
                return _pointsTillUfo;
            }
        }

        public float MinUfoInterval
        {
            get
            {
                return _minUfoInterval;
            }
        }

        public float MaxUfoInterval
        {
            get
            {
                return _maxUfoInterval;
            }
        }
    }
}
   