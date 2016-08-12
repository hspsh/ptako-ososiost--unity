using UnityEngine;
using System.Collections;

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
        birdToControl.FlyForward(Input.GetAxis("Vertical"));
        birdToControl.RotatePitch(Input.GetAxis("Horizontal"));
    }
}
