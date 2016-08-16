using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

    protected Rigidbody rigidbodyComponent;

    public float PtakoPrędkość;
    public float PtakoZwrotność;

	// Use this for initialization
	void Start () {
        rigidbodyComponent = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

	}

    public void FlyForward(float flyingInput) {
        Vector3 direction = (transform.rotation * Vector3.forward) * PtakoPrędkość * flyingInput;
        rigidbodyComponent.AddForce(direction);
    }

    public void RotateRoll(float rotation) {
        rigidbodyComponent.AddTorque(new Vector3(rotation * PtakoZwrotność, 0, 0));
    }

    public void RotatePitch(float rotation) {
        rigidbodyComponent.AddTorque(new Vector3(0, rotation * PtakoZwrotność, 0));
    }

    public void RotateYaw(float rotation) {
        rigidbodyComponent.AddTorque(new Vector3(0, 0, rotation * PtakoZwrotność));
    }
}
