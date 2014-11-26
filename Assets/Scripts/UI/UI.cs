using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UI : MonoBehaviour
    {
        private int _highScore;
        private int _score;

        [SerializeField]
        private Text highScoreLabel;

        [SerializeField]
        private Text scoreLabel = null;

        private int _lifes;
    
        [SerializeField]
        private LifeIconManager LifeIconManager;

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
                if (scoreLabel)
                {
                    scoreLabel.text = _score.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public int HighScore
        {
            get
            {
                return _highScore;
            }
            set
            {
                _highScore = value;

                if (highScoreLabel)
                {
                    highScoreLabel.text = _highScore.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public int Lifes
        {
            get
            {
                return _lifes;
            }
            set
            {
                _lifes = value;
                if (LifeIconManager) {LifeIconManager.SetLives(_lifes);}
            }
        }

        public void ShowGameOver(bool show = true)
        {
            if (_gameOverScreen)
            {
                _gameOverScreen.SetActive(show);
            }
            //Application.LoadLevel(StringConstants.MenuScene);
        }

        [SerializeField]
        private GameObject _gameOverScreen;

    }
}

