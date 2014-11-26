using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class LifeIconManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _liveIconPrefab;

        public int CurrentLifes
        {
            get
            {
                return transform.childCount;
            }
        }

        public void SetLives(int newLifeAmount)
        {
            if (CurrentLifes == newLifeAmount)
            {
                return;
            }

            if (CurrentLifes > newLifeAmount)
            {
                for (int i = 0; i <= CurrentLifes - newLifeAmount; i++)
                {
                    if (Application.isPlaying)
                    {
                        Destroy(transform.GetChild(0).gameObject);
                    }
                    else
                    {
                        DestroyImmediate(transform.GetChild(0).gameObject);
                    }
                }
            }
            else
            {
                for (int i = 0; i <= newLifeAmount - CurrentLifes + 1; i++)
                {
                    if (!_liveIconPrefab)
                    {
                        _liveIconPrefab = new GameObject("Icon");
                    }

                    var go = Instantiate(_liveIconPrefab) as GameObject;
                    go.transform.SetParent(transform);
                }
            }
        }

        void Awake()
        {
            if (!_liveIconPrefab)
            {
                _liveIconPrefab = new GameObject("LiveIconPrefab");
            }
        }
    }
}