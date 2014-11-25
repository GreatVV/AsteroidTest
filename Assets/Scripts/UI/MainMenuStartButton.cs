using UnityEngine;

public class MainMenuStartButton : MonoBehaviour
{
    public void OnClick()
    {
        Application.LoadLevel(StringConstants.GameScene);
    }
}