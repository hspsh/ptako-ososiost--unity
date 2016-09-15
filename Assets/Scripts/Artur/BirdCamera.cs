using UnityEngine;
using System.Collections;

public class BirdCamera : MonoBehaviour {

    public Transform target;
    public float walkDistance;
    public float runDistance;
    public float height;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;

    private Transform camTransform;
    private float x;
    private float y;
    private bool camButtonDown = false;

	// Use this for initialization
	void Start () {
        if (target == null)
            Debug.LogWarning("We have no Target !!!");

        camTransform = transform;
        CameraSetUp();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            camButtonDown = true;
        } else if (Input.GetMouseButtonUp(1))
        {
            camButtonDown = false;
        }

    }

    void LateUpdate () {

        if (camButtonDown)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f + height, -walkDistance) + target.position;
            
            camTransform.rotation = rotation;
            camTransform.position = position;

        } else
        {
            //  Vector3 moveCamTo = target.position;

            //  Vector3 rotateCamTo = target.eulerAngles;

            // float bias = 0.96f;
            // transform.position = transform.position * bias + moveCamTo * (1.0f - bias);
            // transform.rotation = target.rotation;
            //transform.LookAt(target.position + target.forward * 10.0f);

            Debug.Log("" + target.localRotation);
            Quaternion rotation = Quaternion.Euler(target.localRotation.x, target.localRotation.y, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f + height, -walkDistance) + target.position;

            //transform.LookAt(target.position + target.forward * 10.0f);
            camTransform.localRotation = rotation;
            camTransform.position = position;

            
        }
    } 

    public void CameraSetUp()
    {
        camTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
        camTransform.LookAt(target);
    }
}
