using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float speed;
    public float deleyTimeWingsDown;
    public float speedWings;

    private Rigidbody rigid;
    private float wingsInMove = 1.0f;
    private Transform leftFin;
    private Transform rightFin;
    private float leftFinIsDown = 1.0f;
    private float rightFinIsDown = 1.0f;
    private bool leftFinIsDownBool = false;
    private bool rightFinIsDownBool = false;


    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        leftFin = transform.GetChild(0).transform;
        rightFin = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void Update()
    {



        transform.position += (transform.forward * speed) * Time.deltaTime;

        controllSpeedWingsInMove();

        controllFinIsDown();


        speedUpAndSlowDownWhenGoingDownAndUp();

        speedUpWhenUsingWings();


        useGravityWhenNotEnoughSpeed();

        settingUpMaxAndLowSpeed();


        cannotCrossTerrainCollider();

        Debug.Log("Da : " + deleyTimeWingsDown);
        changingFlyDirection();

    }

    private void changingFlyDirection()
    {
        transform.Rotate(Input.GetAxis("Vertical") * 3.0f, 0.0f, -Input.GetAxis("Horizontal") * 4.0f);
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

    private void controllFinIsDown()
    {
        if (rightFinIsDown > -2)
        {
            rightFinIsDown -= Time.deltaTime;
        }

        if (leftFinIsDown > -2)
        {
            leftFinIsDown -= Time.deltaTime;
        }
    }

    private void speedUpWhenUsingWings()
    {

        bool fKeyDown = Input.GetKeyDown(KeyCode.F);
        bool fKeyUp = Input.GetKeyUp(KeyCode.F);
        bool gKeyDown = Input.GetKeyDown(KeyCode.G);
        bool gKeyUp = Input.GetKeyUp(KeyCode.G);
        Debug.Log("fin : " + rightFinIsDown);

        if ((fKeyDown || fKeyUp) && (gKeyUp || gKeyDown) && (leftFinIsDownBool == false && leftFinIsDownBool == false))
        {
            flyFasterWhileWingsInMove();
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

    private void flyFasterWhileWingsInMove()
    {
        wingsInMove += speedWings;
        leftFin.Rotate(new Vector3(0.0f, 0.0f, 20.0f));
        rightFin.Rotate(new Vector3(0.0f, 0.0f, 20.0f));
        leftFinIsDown = deleyTimeWingsDown;
        rightFinIsDown = deleyTimeWingsDown;
        leftFinIsDownBool = true;
        rightFinIsDownBool = true;
    }

    private void controllSpeedWingsInMove()
    {
        if (wingsInMove > 20.0f)
        {
            wingsInMove = 20.0f;
        }
        else if (wingsInMove > 0.0f)
        {
            wingsInMove -= Time.deltaTime;
        }
        else if (wingsInMove < 0.0f)
        {
            wingsInMove = 0.0f;
        }
    }

    private void speedUpAndSlowDownWhenGoingDownAndUp()
    {
        if (transform.forward.y == 0)
        {

        }
        else if (transform.forward.y > 0)
        {
            rigid.useGravity = true;
            if (wingsInMove > 1.0f)
            {
                speed -= (transform.forward.y * 20.0f) * Time.deltaTime;
                speed += wingsInMove * Time.deltaTime;
            }
            else if (wingsInMove < 1.0f)
            {
                speed -= (transform.forward.y * 20.0f) * Time.deltaTime;
            }

        }
        else if (transform.forward.y < 0)
        {
            if (wingsInMove > 1.1f)
            {
                speed -= (transform.forward.y * 20.0f * 2.0f) * Time.deltaTime;
                speed += wingsInMove * Time.deltaTime;
            }
            else if (wingsInMove < 1.1f)
            {
                speed -= (transform.forward.y * 20.0f * 2.0f) * Time.deltaTime;
            }

        }
    }

    private void useGravityWhenNotEnoughSpeed()
    {
        if (speed > 15)
        {
            rigid.useGravity = false;
        }
    }

    private void settingUpMaxAndLowSpeed()
    {
        if (speed < 1 && wingsInMove < 1.1f)
        {
            speed = 1;
        }
        else if (speed > 40 && wingsInMove < 1.1f) // TODO: Repeir if comes from 300 to 200 too fast
        {
            speed = 40;
        }
        else if (speed > 70 && wingsInMove > 1.1f)
        {
            speed = 70;
        }
    }

}
