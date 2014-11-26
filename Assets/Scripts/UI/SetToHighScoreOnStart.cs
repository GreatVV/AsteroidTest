using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    [RequireComponent(typeof (Text))]
    public class SetToHighScoreOnStart : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Text>().text = string.Format(
                                                      "High score: {0}",
                                                      PlayerPrefs.GetInt(StringConstants.HighScorePref, 0));
        }
    }
}