using UnityEngine;
using System.Collections;

//Klasa reprezentująca ptaka który będzie sobie latać
public class Bird : MonoBehaviour {

    protected Rigidbody rigidbodyComponent;

    public float PtakoPrędkość;
    public float PtakoZwrotność;

	// Use this for initialization
	void Start () {
        rigidbodyComponent = GetComponent<Rigidbody>();
	}

    public void FlyForward(float flyingInput) {
        Vector3 direction = (transform.rotation * Vector3.forward) * PtakoPrędkość * flyingInput;
        rigidbodyComponent.AddForce(direction);
    }

    public void RotateRoll(float rotation) {
        Vector3 rotationVector = new Vector3(rotation * PtakoZwrotność, 0, 0);
        Vector3 globalRotationVector = transform.rotation * rotationVector;
        rigidbodyComponent.AddTorque(globalRotationVector);
    }

    public void RotatePitch(float rotation) {
        Vector3 rotationVector = new Vector3(0, rotation * PtakoZwrotność, 0);
        Vector3 globalRotationVector = transform.rotation * rotationVector;
        rigidbodyComponent.AddTorque(globalRotationVector);
    }

    public void RotateYaw(float rotation) {
        Vector3 rotationVector = new Vector3(0, 0, rotation * PtakoZwrotność);
        Vector3 globalRotationVector = transform.rotation * rotationVector;
        rigidbodyComponent.AddTorque(globalRotationVector);
    }
}
