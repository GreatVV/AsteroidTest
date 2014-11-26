using Game.Players.Control;
using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class AccelerometerTest
{
    [Test]
    public void Zero()
    {
        var accelerometer = new Vector3(0, 0, -1);

        var control =
            new GameObject("acelerometerControl", typeof (AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(0).Within(0.01f));
    }

    [Test]
    public void ForwardTest()
    {
        var accelerometer = new Vector3(0, 0.2f, -1);

        var control =
            new GameObject("acelerometerControl", typeof(AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(0).Within(0.01f));
    }

    [Test]
    public void LeftTest()
    {
        var accelerometer = new Vector3(-0.2f, 0, -1);

        var control =
            new GameObject("acelerometerControl", typeof(AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(90).Within(0.01f));
    }

    [Test]
    public void RightTest()
    {
        var accelerometer = new Vector3(0.2f, 0, -1);

        var control =
            new GameObject("acelerometerControl", typeof(AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(270).Within(0.01f));
    }

    [Test]
    public void DownTest()
    {
        var accelerometer = new Vector3(0, -0.2f, -1);

        var control =
            new GameObject("acelerometerControl", typeof(AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(180).Within(0.01f));
    }

    [Test]
    public void UpTest()
    {
        var accelerometer = new Vector3(0, 0.2f, -1);

        var control =
            new GameObject("acelerometerControl", typeof(AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(0).Within(0.01f));
    }

    [Test]
    public void UpLeftTest()
    {
        var accelerometer = new Vector3(-0.2f, 0.2f, -1);

        var control =
            new GameObject("acelerometerControl", typeof(AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(45).Within(0.01f));
    }

    [Test]
    public void UpRightTest()
    {
        var accelerometer = new Vector3(0.2f, 0.2f, -1);

        var control =
            new GameObject("acelerometerControl", typeof(AccelerometerControl)).GetComponent<AccelerometerControl>();

        var rotation = control.RotationFor(accelerometer);

        Assert.That(rotation.eulerAngles.z, Is.EqualTo(315).Within(0.01f));
    }
}