

using UnityEngine;


public class SpeedController : MonoBehaviour
{
    public float speed; // initial speed ( it changes when program run )
    public float speedUpPerSecond;

    public float divingDownSpeed; // how much speed up when bird diving down
    public float divingUpSpeed; // how much speed up when bird diving up

    public float wingsSpeed;// adding speed or 0 if wings are not moving long time
    public float wingsMaxSpeed;
    public float wingsInMoveSpeed; // How much speed will up when u using wings ( it depend what % of angle u did on wings so 100% is wingsSpeed += wingsInMoveSpeed)
    public float reduceWingsSpeed; // How much reduce wingsSpeed per second

    public float wingsDivingSpeed { get; set; } // adding speed or 0 if wings are not in diving posision
    public float wingsInDivingPositionSpeed; // How much u speed up per second when wings are in diving posision ( it depend what % of angle are on wings so 100% is wingsDivingSpeed = wingsInDivingPositionSpeed )

    private Rigidbody rigid;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

    }

    void Update()
    {
        speedUpAndSlowDownWhenDivingDownAndUp();
        addAllSpeeds();
        reduceSpeedWingsInMove();
        speedController();

        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void addAllSpeeds()
    {
        speed += (wingsSpeed * Time.deltaTime) + (wingsDivingSpeed * Time.deltaTime);
    }

    private void speedController()
    {
        if (speed < 10)
        {
            rigid.useGravity = true;
        }

        if (speed > 100)
        {
            speed = 100.0f;
        }

        if (speed < 0)
        {
            speed = 0.0f;
        }

        if (speed > 10)
        {
            rigid.useGravity = false;
        }
    }

    private void speedUpAndSlowDownWhenDivingDownAndUp()
    {

        if (transform.forward.y == 0)
        {

        }
        else if (transform.forward.y > 0)
        {
            float divingUp = transform.forward.y * divingUpSpeed;
            speed -= divingUp * Time.deltaTime;
            speedPerSecond(divingUp);
        }
        else if (transform.forward.y < 0)
        {
            float divingDown = transform.forward.y * divingDownSpeed;
            speed -= divingDown * Time.deltaTime;
            speedPerSecond(divingDown);
        }

    }

    public void speedPerSecond(float divingUpOrDown)
    {
        speedUpPerSecond = -divingUpOrDown + wingsSpeed + wingsDivingSpeed;
    }

    public void speedUpWhenWingsInMove(float angle, float delayAngle)
    {

        if (angle > delayAngle)
        {
            wingsSpeed += wingsInMoveSpeed * angle;
        }

    }

    private void reduceSpeedWingsInMove()
    {
        if (wingsSpeed > 0.0f)
        {
            wingsSpeed -= reduceWingsSpeed * Time.deltaTime;
        }
        else if (wingsSpeed < 0.0f)
        {
            wingsSpeed = 0.0f;
        }

        if (wingsSpeed > wingsMaxSpeed)
        {
            wingsSpeed = wingsMaxSpeed;
        }

    }

    public void speedUpWhenWingsInDivingPosition(float angle, float delayAngle)
    {

        if (angle > delayAngle)
        {
            wingsDivingSpeed = wingsInDivingPositionSpeed * angle;

        }
        else if (angle < delayAngle)
        {
            wingsDivingSpeed = 0;

        }

    }

}
