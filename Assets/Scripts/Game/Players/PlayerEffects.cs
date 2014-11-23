using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEffects : MonoBehaviour
{
    private Player player;

    [SerializeField]
    private GameObject IndestructableEffectPrefab;

    private GameObject _indestuctableEffect;

    void Start()
    {
        player = GetComponent<Player>();
        player.IndestructableStateChanged += OnDestructableStateChanged;
        OnDestructableStateChanged(player.IsUndestructable);
    }
    void OnDestructableStateChanged(bool state)
    {
        if (!_indestuctableEffect && IndestructableEffectPrefab)
        {
            _indestuctableEffect = (GameObject)Instantiate(IndestructableEffectPrefab);
            _indestuctableEffect.transform.parent = transform;
        }

        if (_indestuctableEffect)
        {
            _indestuctableEffect.SetActive(state);
        }
    }
}