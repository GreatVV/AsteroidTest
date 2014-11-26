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

    

        public static GameLogicParameters Instance
        {
            get
            {
                if (!_instance)
                {
                    Debug.LogWarning("There is no instance of gamelogic parameters - create new one");
                    _instance = CreateInstance<GameLogicParameters>();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public static float PlayerRotateSpeed
        {
            get
            {
                return Instance._playerRotateSpeed;
            }
        }

        public static int DefaultNumberOfNewAsteroids
        {
            get
            {
                return Instance._defaultNumberOfNewAsteroids;
            }
        }

        public static float DefaultPlayerSpeed
        {
            get
            {
                return Instance._defaultPlayerSpeed;
            }
        }

        public static int StartNumberOfLifes
        {
            get
            {
                return Instance._startNumberOfLifes;
            }
        }

        public static float UndestructableTime
        {
            get
            {
                return Instance._undestructableTime;
            }
        }

        public static int PointsTillUfo
        {
            get
            {
                return Instance._pointsTillUfo;
            }
        }

        public static float MinUfoInterval
        {
            get
            {
                return Instance._minUfoInterval;
            }
        }

        public static float MaxUfoInterval
        {
            get
            {
                return Instance._maxUfoInterval;
            }
        }
    }
}
   