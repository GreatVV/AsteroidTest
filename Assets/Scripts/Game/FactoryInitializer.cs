using UnityEngine;

public class FactoryInitializer : MonoBehaviour
{
    [SerializeField]
    private EnemyFactory _enemyFactory = null;

    [SerializeField]
    private PlayerFactory playerFactory = null; 

    void Awake()
    {
        EnemyFactory.Instance = _enemyFactory;
        PlayerFactory.Instance = playerFactory;
    }
}