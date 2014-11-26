using UnityEngine;
using Utils;

namespace UI
{
    public class MainMenuStartButton : MonoBehaviour
    {
        public void OnClick()
        {
            Application.LoadLevel(StringConstants.GameScene);
        }
    }
}