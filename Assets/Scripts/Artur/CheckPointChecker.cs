using UnityEngine;

public class CheckPointChecker : MonoBehaviour
{

    public Transform birdPosition;
    public Transform target;

    private LineRenderer line;
    private float distanceToCheck = 6f;
    private int targetNumber = 1;

    public Vector3 distance;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        line.SetPosition(1, target.position);
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, birdPosition.position);

        if (CheckDistance(birdPosition.position, target.position))
        {
            if (targetNumber <= 7)
            {
                target = GameObject.Find("CheckPoint (" + targetNumber + ")").GetComponent<Transform>();
                line.SetPosition(1, target.position);
                targetNumber++;
            }
            else if (targetNumber > 7)
            {
                // TODO: LoadScene
            }
        }


        distance = birdPosition.position - target.position;
    }

    private bool CheckDistance(Vector3 me, Vector3 targ)
    {
        float y = me.y - targ.y;
        float x = me.x - targ.x;
        float z = me.z - targ.z;

        if ((y < distanceToCheck && y > -distanceToCheck) && (x < distanceToCheck && x > -distanceToCheck) && (z < distanceToCheck && z > -distanceToCheck))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
