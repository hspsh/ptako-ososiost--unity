using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BirdController : MonoBehaviour
{

    public float deleyTimeWingsDown;
    public float reduceDeleyTimeWingsDown;
    public float rotateFowardSensitive;
    public float rotateRightSensitive;

    public float rightFinAngle { get; set; }
    public float leftFinAngle { get; set; }
    public float trigerMoveWingsAngle { get; set; }
    public float divingAngle { get; set; }
    public float trigerDivingAngle { get; set; }
    public bool spaceDown { get; set; }
    public bool spaceUp { get; set; }

    private Transform leftFin;
    private Transform rightFin;
    private SpeedController speedController;

    private float leftFinIsDown = 1.0f;
    private float rightFinIsDown = 1.0f;
    private bool leftFinIsDownBool = false;
    private bool rightFinIsDownBool = false;

    private List<string> listRotation = new List<string>();

    private bool rightFinRotatedDown = false;
    private bool rightFinRotatedUp = false;
    private bool leftFinRotatedDown = false;
    private bool leftFinRotatedUp = false;


    // Use this for initialization
    void Start()
    {
        leftFin = transform.GetChild(0).transform;
        rightFin = transform.GetChild(1).transform;
        spaceDown = true;

        speedController = GetComponent<SpeedController>();
    }

    // Update is called once per frame
    void Update()
    {

        cannotCrossTerrainCollider();

        speedUpWhenDivingByWings();

        changeFlyDirection();

        finRotate();

        delayTimeWings();

    }

    private void endingRotationsBefour(string rotationName)
    {

        for (int i = listRotation.IndexOf(rotationName) - 1; i >= 0; i--)
        {
            string a = listRotation.ElementAt(i);
            endingRotations(a, i);
        }
    }

    private void endingRotationsTill(string rotationName)
    {

        if (listRotation.Exists(x => x.Equals(rotationName)))
        {
            int i = listRotation.IndexOf(rotationName);
            while (listRotation.Count > i)
            {
                string a = listRotation.ElementAt(listRotation.Count - 1);
                endingRotations(a, listRotation.Count - 1);
            }
        }


    }

    private void endingRotations(string rotationName, int deleteIndex)
    {

        if (rotationName == "leftFinRotatedDown")
        {
            leftFin.Rotate(Vector3.right * 10);
            listRotation.RemoveAt(deleteIndex);
            return;
        }
        if (rotationName == "leftFinRotatedUp")
        {
            leftFin.Rotate(Vector3.right * -10);
            listRotation.RemoveAt(deleteIndex);
            return;
        }
        if (rotationName == "rightFinRotatedDown")
        {
            rightFin.Rotate(Vector3.right * -10);
            listRotation.RemoveAt(deleteIndex);
            return;
        }
        if (rotationName == "rightFinRotatedUp")
        {
            rightFin.Rotate(Vector3.right * 10);
            listRotation.RemoveAt(deleteIndex);
            return;
        }


        if (rotationName == "speedUpDivingBool")
        {
            leftFin.Rotate(new Vector3(0.0f, 60.0f, 0.0f));
            rightFin.Rotate(new Vector3(0.0f, -60.0f, 0.0f));
            listRotation.RemoveAt(deleteIndex);
            return;
        }

    }

    private void finRotate()
    {

        if (-leftFinAngle > 0 && !leftFinRotatedDown)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                leftFin.Rotate(Vector3.right * -10);
                leftFinRotatedDown = true;
                listRotation.Add("leftFinRotatedDown");
            }
        }
        else if (leftFinRotatedDown && leftFinAngle == 0)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                endingRotationsTill("leftFinRotatedDown");
                leftFinRotatedDown = false;

            }
        }

        if (leftFinAngle > 0 && !leftFinRotatedUp)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                leftFin.Rotate(Vector3.right * 10);
                leftFinRotatedUp = true;
                listRotation.Add("leftFinRotatedUp");
            }
        }
        else if (leftFinRotatedUp && leftFinAngle == 0)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                endingRotationsTill("leftFinRotatedUp");
                leftFinRotatedUp = false;

            }
        }

        if (-rightFinAngle > 0 && !rightFinRotatedDown)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                rightFin.Rotate(Vector3.right * 10);
                rightFinRotatedDown = true;
                listRotation.Add("rightFinRotatedDown");
            }
        }
        else if (rightFinRotatedDown && rightFinAngle == 0)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                endingRotationsTill("rightFinRotatedDown");
                rightFinRotatedDown = false;
            }
        }

        if (rightFinAngle > 0 && !rightFinRotatedUp)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                rightFin.Rotate(Vector3.right * -10);
                rightFinRotatedUp = true;
                listRotation.Add("rightFinRotatedUp");
            }
        }
        else if (rightFinRotatedUp && rightFinAngle == 0)
        {
            if (!leftFinIsDownBool && !rightFinIsDownBool)
            {
                endingRotationsTill("rightFinRotatedUp");
                rightFinRotatedUp = false;
            }
        }

    }

    private void changeFlyDirection()
    {

        if ((rightFinAngle > trigerMoveWingsAngle || -rightFinAngle > trigerMoveWingsAngle) || (leftFinAngle > trigerMoveWingsAngle || -leftFinAngle > trigerMoveWingsAngle))
        {

            if ((rightFinAngle > trigerMoveWingsAngle && leftFinAngle > trigerMoveWingsAngle) || (-rightFinAngle > trigerMoveWingsAngle && -leftFinAngle > trigerMoveWingsAngle))
            {

                if (leftFinAngle > 0 && rightFinAngle > 0)
                {
                    transform.Rotate(Vector3.right * (leftFinAngle + rightFinAngle) * rotateFowardSensitive * Time.deltaTime);
                }
                else if (-leftFinAngle > 0 && -rightFinAngle > 0)
                {
                    transform.Rotate(Vector3.right * (-leftFinAngle + -rightFinAngle) * -rotateFowardSensitive * Time.deltaTime);
                }

            }
            else
            {
                if (rightFinAngle > trigerMoveWingsAngle)
                {
                    transform.Rotate(Vector3.forward * -rightFinAngle * rotateRightSensitive * Time.deltaTime);
                }
                if (-rightFinAngle > trigerMoveWingsAngle)
                {
                    transform.Rotate(Vector3.forward * -rightFinAngle * rotateFowardSensitive * Time.deltaTime);
                }
                if (-leftFinAngle > trigerMoveWingsAngle)
                {
                    transform.Rotate(Vector3.forward * leftFinAngle * rotateRightSensitive * Time.deltaTime);
                }
                if (leftFinAngle > trigerMoveWingsAngle)
                {
                    transform.Rotate(Vector3.forward * leftFinAngle * rotateFowardSensitive * Time.deltaTime);
                }
            }

        }

    }


    private void cannotCrossTerrainCollider()
    {
        float terrainHeightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

        if (terrainHeightWhereWeAre > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x,
                terrainHeightWhereWeAre, transform.position.z);
        }
    }

    public void speedUpWhenUsingWings(float wingsAngle, float trigerAngle)
    {

        bool finsArentRotate = (!rightFinRotatedDown && !rightFinRotatedUp && !leftFinRotatedDown && !leftFinRotatedUp);
        bool finsArentDown = (!leftFinIsDownBool && !rightFinIsDownBool);
        bool wingsArentDiving = (divingAngle < trigerDivingAngle);

        if (wingsAngle > trigerAngle && finsArentDown && finsArentRotate && wingsArentDiving)
        {
            flyFasterWhileWings(wingsAngle, trigerAngle);
        }
        else if (rightFinIsDown <= 0.0f && rightFinIsDownBool == true)
        {
            rightFin.Rotate(new Vector3(0.0f, 0.0f, -20.0f));
            rightFinIsDownBool = false;
        }
        else if (leftFinIsDown <= 0.0f && leftFinIsDownBool == true)
        {
            leftFin.Rotate(new Vector3(0.0f, 0.0f, -20.0f));
            leftFinIsDownBool = false;
        }

    }

    private void flyFasterWhileWings(float wingsAngle, float trigerAngle)
    {

        leftFin.Rotate(new Vector3(0.0f, 0.0f, 20.0f));
        rightFin.Rotate(new Vector3(0.0f, 0.0f, 20.0f));
        leftFinIsDown = deleyTimeWingsDown;
        rightFinIsDown = deleyTimeWingsDown;
        leftFinIsDownBool = true;
        rightFinIsDownBool = true;
        SpeedController addWingsSpeed = GetComponent<SpeedController>();
        addWingsSpeed.speedUpWhenWingsInMove(wingsAngle, trigerAngle);
    }

    private void delayTimeWings()
    {
        if (leftFinIsDown > 0.0f)
        {
            leftFinIsDown -= reduceDeleyTimeWingsDown * Time.deltaTime;
        }
        else if (leftFinIsDown < 0.0f)
        {
            leftFinIsDown = 0.0f;
        }

        if (rightFinIsDown > 0.0f)
        {
            rightFinIsDown -= reduceDeleyTimeWingsDown * Time.deltaTime;
        }
        else if (rightFinIsDown < 0.0f)
        {
            rightFinIsDown = 0.0f;
        }
    }



    private void speedUpWhenDivingByWings()
    {
        bool finsArentMoving = (!leftFinIsDownBool && !rightFinIsDownBool);

        if (divingAngle > trigerDivingAngle && spaceDown && finsArentMoving)
        {
            listRotation.Add("speedUpDivingBool");
            endingRotationsBefour("speedUpDivingBool");
            leftFin.Rotate(new Vector3(0.0f, -60.0f, 0.0f));
            rightFin.Rotate(new Vector3(0.0f, 60.0f, 0.0f));
            speedController.speedUpWhenWingsInDivingPosition(divingAngle, trigerDivingAngle);
            spaceUp = true;
            spaceDown = false;
        }
        else if (divingAngle < trigerDivingAngle && spaceUp)
        {
            endingRotationsTill("speedUpDivingBool");
            speedController.speedUpWhenWingsInDivingPosition(divingAngle, trigerDivingAngle);
            spaceUp = false;
            spaceDown = true;
        }

    }

}
