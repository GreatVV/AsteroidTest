using System.Linq;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class UITest 
{
    
    [Test]
    public void SetDefaultOnGameStart()
    {
        var ui = new GameObject("ui", typeof(UI)).GetComponent<UI>();

        var session = new GameObject("session", typeof(Session)).GetComponent<Session>();
        session.UI = ui;
        session.HighScore = 100;
        session.Score = 0;

        Assert.AreEqual(0, ui.Score);
        Assert.AreEqual(100, ui.HighScore);
    }

    [Test]
    public void SetLifes()
    {
        
        var ui = new GameObject("ui", typeof(UI)).GetComponent<UI>();

        var session = new GameObject("session", typeof (Session)).GetComponent<Session>();
        session.HighScore = 100;
        session.UI = ui;
        session.Restart();
        session.HighScore = 100;

        Assert.AreEqual(GameLogicParameters.StartNumberOfLifes, ui.Lifes);
        Assert.AreEqual(session.HighScore, ui.HighScore);
        Assert.AreEqual(100, ui.HighScore);
    }

    [Test]
    public void LifeIconManagerTest()
    {
        var lifeIconManager = new GameObject("lifeIconManager", typeof (LifeIconManager)).GetComponent<LifeIconManager>();

        Assert.AreEqual(0, lifeIconManager.CurrentLifes);

        lifeIconManager.SetLives(3);

        Assert.AreEqual(3, lifeIconManager.CurrentLifes);

        lifeIconManager.SetLives(2);
        Assert.AreEqual(2, lifeIconManager.CurrentLifes);

        lifeIconManager.SetLives(4);
        Assert.AreEqual(4, lifeIconManager.CurrentLifes);
    }

    [Test]
    public void AddPointsTest()
    {
        var ui = new GameObject("ui", typeof(UI)).GetComponent<UI>();
        
        var session = new GameObject("session", typeof(Session)).GetComponent<Session>();
        session.UI = ui;

        session.OnDestroyed(new Asteroid()
                            {
                                TimeToDivide = 3
                            });

        Assert.AreEqual(10, session.Score);
        Assert.AreEqual(10, ui.Score);
    }

    [Test]
    public void SessionSaveTest()
    {
        PlayerPrefs.DeleteAll();

        var session = new GameObject("session", typeof(Session)).GetComponent<Session>();
        session.HighScore = 100;
        session.SaveToPrefs();

        Assert.AreEqual(100, PlayerPrefs.GetInt(StringConstants.HighScorePref));

        session.HighScore = 90;
        session.SaveToPrefs();

        Assert.AreEqual(100, PlayerPrefs.GetInt(StringConstants.HighScorePref));

        session.HighScore = 101;
        session.SaveToPrefs();

        Assert.AreEqual(101, PlayerPrefs.GetInt(StringConstants.HighScorePref));

        PlayerPrefs.DeleteAll();
    }
}
