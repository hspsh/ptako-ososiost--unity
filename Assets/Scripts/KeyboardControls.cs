using UnityEngine;
using System.Collections;


//Kontroler do ptaka, na razie czytający input z klawiatury
//TODO czytanie inputu z magicznych urządzeń elektronicznych
public class KeyboardControls : MonoBehaviour {

    public Bird birdToControl;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        ProcessMovement();
    }
    void ProcessMovement() {
        birdToControl.RotateRoll(Input.GetAxis("Vertical"));

        birdToControl.RotatePitch(Input.GetAxis("Horizontal"));
        birdToControl.RotateYaw(Input.GetAxis("RotateRight"));
    }
}
