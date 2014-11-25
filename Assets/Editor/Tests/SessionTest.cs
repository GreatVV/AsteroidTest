using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class SessionTest
{
    [Test]
    public void CreateUfoTest()
    {
        var session = new GameObject("session", typeof (Session)).GetComponent<Session>();

        var field = new GameObject("field", typeof (Field)).GetComponent<Field>();

        session.Field = field;

        session.CreateUfoAfter(30);
    }
}