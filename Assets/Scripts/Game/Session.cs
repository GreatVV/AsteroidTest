using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Session : MonoBehaviour
{
    [SerializeField]
    private Field _field;
    private int _lifes;
    [SerializeField]
    public UI UI;
    private int _highScore;
    private int _score;

    public Field Field
    {
        get
        {
            return _field;
        }
        set
        {
            if (_field)
            {
                UnSubscribeForFieldEvents();
            }
            _field = value;
            if (_field)
            {
                SubscribeForFieldEvents();
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
            if (UI)
            {
                UI.HighScore = _highScore;
            }
        }
    }

    public int Score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (Score > HighScore)
            {
                HighScore = Score;
            }
            if (UI)
            {
                UI.Score = _score;
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
            if (UI)
            {
                UI.Lifes = _lifes;
            }
        }
    }

    private void SubscribeForFieldEvents()
    {
        UnSubscribeForFieldEvents();
        if (Field)
        {
            Field.Destroyed += OnDestroyed;
        }
    }

    void OnDestroy()
    {
        UnSubscribeForFieldEvents();
    }

    private void UnSubscribeForFieldEvents()
    {
        if (Field)
        {
            Field.Destroyed -= OnDestroyed;
        }
    }

    public void OnDestroyed(IMovable movable)
    {
        if (movable is Player)
        {
            Lifes--;
            if (Lifes <= 0)
            {
                if (UI)
                {
                    StopAllCoroutines();
                    SaveToPrefs();
                    UI.ShowGameOver();
                }
            }
            else
            {
                Field.SpawnPlayer();
            }
        }
        else
        {
            Score += PointManager.Instance.GetPointsForMovable(movable);
        }
    }

    public void Restart()
    {
        SaveToPrefs();

        StopAllCoroutines();
        StartCoroutine(CreateUfoAfter(30));

        if (UI)
        {
            UI.ShowGameOver(false);
        }

        Score = 0;
        HighScore = PlayerPrefs.GetInt(StringConstants.HighScorePref, 0);
        Lifes = GameLogicParameters.StartNumberOfLifes;

        if (Field)
        {
            Field.StartGame();
        }
    }

    public void SaveToPrefs()
    {
        if (PlayerPrefs.GetInt(StringConstants.HighScorePref, 0) < Score)
        {
            PlayerPrefs.SetInt(StringConstants.HighScorePref, Score);
            PlayerPrefs.Save();
        }
    }

    public void Start()
    {
        if (!Field)
        {
            Field = FindObjectOfType<Field>();
        }
        else
        {
            SubscribeForFieldEvents();
        }
        
        Restart();
    }

    public IEnumerator CreateUfoAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Field.CreateUfo();
        StartCoroutine(CreateUfoAfter(Random.Range(GameLogicParameters.MinUfoInterval, GameLogicParameters.MaxUfoInterval)));
    }
}