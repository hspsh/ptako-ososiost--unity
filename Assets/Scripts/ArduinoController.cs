using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

//Kontroler do ptaka interpretujący sygnały z arduino
public class ArduinoController : MonoBehaviour {

	public Bird birdToControl;

	public float ArduinoScaleValue;

	protected SerialPort stream;

	protected Vector3 currentRotVector = new Vector3();
	protected Vector3 speedVector = new Vector3();
	protected Vector3 rotationVector = new Vector3();

	void Start () {
		stream = new SerialPort("/dev/ttyACM0", 9600);
		stream.ReadTimeout = 50;
		stream.Open();
	}


	// Update is called once per frame
	void Update () {
		StartCoroutine
		(
		    AsynchronousReadFromArduino
		    (   (string s) => ProcessArduinoInput(s),   // Callback
		        () => {}, 		// Error callback
		        10f                             		// Timeout (seconds)
		    )
		);
	}

	public string ReadFromArduino (int timeout = 0) {
	stream.ReadTimeout = timeout;
	try {
			return stream.ReadLine();
	}
	catch (TimeoutException) {
			return null;
	}
	}

	public void WriteToArduino(string message) {
		stream.WriteLine(message);
        stream.BaseStream.Flush();
	}

	public IEnumerator AsynchronousReadFromArduino(Action<string> callback, Action fail = null, float timeout = float.PositiveInfinity) {

	DateTime initialTime = DateTime.Now;
    DateTime nowTime;
    TimeSpan diff = default(TimeSpan);

    string dataString = null;

    do {
        try {
            dataString = stream.ReadLine();
        }
        catch (TimeoutException) {
            dataString = null;
        }

        if (dataString != null)
        {
            callback(dataString);
            yield return null;
        } else
            yield return new WaitForSeconds(0.05f);

        nowTime = DateTime.Now;
        diff = nowTime - initialTime;

    } while (diff.Milliseconds < timeout);

    if (fail != null)
        fail();
    yield return null;
	}

	protected void ProcessArduinoInput(string values) {
		string[] rotValues = values.Split(',');
		Vector3 rotVector = new Vector3(
			float.Parse(rotValues[0]),
			float.Parse(rotValues[1]),
			float.Parse(rotValues[2])
		);
		Vector3 finalRotVector = new Vector3(
			rotVector.x / ArduinoScaleValue,
			rotVector.y / ArduinoScaleValue,
			rotVector.z / ArduinoScaleValue
		);
		//birdToControl.FlyForward(5);
		Debug.Log(finalRotVector);
		birdToControl.RotateRoll(finalRotVector.x);
		birdToControl.RotatePitch(finalRotVector.y);
		birdToControl.RotateYaw(finalRotVector.z);
	}
}
