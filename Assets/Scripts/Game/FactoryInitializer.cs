using UnityEngine;

public class FactoryInitializer : MonoBehaviour
{
    [SerializeField]
    private EnemyFactory _enemyFactory = null;

    [SerializeField]
    private PlayerFactory playerFactory = null;

    [SerializeField]
    private GameLogicParameters gameLogicParameters = null; 

    void Awake()
    {
        EnemyFactory.Instance = _enemyFactory;
        PlayerFactory.Instance = playerFactory;
        GameLogicParameters.Instance = gameLogicParameters;
    }
}