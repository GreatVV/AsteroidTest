using Game.Scriptable;
using UnityEngine;

namespace Game.Misc
{
    public class FactoryInitializer : MonoBehaviour
    {
        [SerializeField]
        private EnemyFactory _enemyFactory = null;

        [SerializeField]
        private PlayerFactory playerFactory = null;

        [SerializeField]
        private GameLogicParameters gameLogicParameters = null;

        [SerializeField]
        private PointManager pointManager = null; 

        void Awake()
        {
            EnemyFactory.Instance = _enemyFactory;
            PlayerFactory.Instance = playerFactory;
            GameLogicParameters.Instance = gameLogicParameters;
            PointManager.Instance = pointManager;
        }
    }
}