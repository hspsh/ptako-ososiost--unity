using UnityEngine;


public class InputController : MonoBehaviour
{

    BirdController bird;

    // Use this for initialization
    void Start()
    {
        bird = transform.GetComponent<BirdController>();
        bird.trigerMoveWingsAngle = 0.5f;
        bird.trigerDivingAngle = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        uBtnInput(); // rotate left fin go up
        iBtnInput(); // rotate right fin go up
        jBtnInput(); // rotate left fin go down
        kBtnInput(); // rotate right fin go down

        fgBtnInput(); // move wings to fly faster

        spaceBtnInput(); // wings in diving position

    }

    private void spaceBtnInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bird.divingAngle = 1.0f;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            bird.divingAngle = 0.1f;
        }
    }

    private void fgBtnInput()
    {
        bool fKeyDown = Input.GetKeyDown(KeyCode.F);
        bool fKeyUp = Input.GetKeyUp(KeyCode.F);
        bool gKeyDown = Input.GetKeyDown(KeyCode.G);
        bool gKeyUp = Input.GetKeyUp(KeyCode.G);

        if (((fKeyDown && gKeyUp) || (fKeyUp && gKeyDown) || (fKeyUp && gKeyUp)))
        {
            bird.speedUpWhenUsingWings(1.0f, 0.2f);
        }
        else
        {
            bird.speedUpWhenUsingWings(0.1f, 0.2f);
        }
    }

    private void uBtnInput()
    {

        bool isDownU = Input.GetKeyDown(KeyCode.U);
        bool isUpU = Input.GetKeyUp(KeyCode.U);

        if (isDownU)
        {
            bird.leftFinAngle = 1.0f;
        }
        if (isUpU)
        {
            bird.leftFinAngle = 0.0f;
        }

    }

    private void iBtnInput()
    {

        bool isDownI = Input.GetKeyDown(KeyCode.I);
        bool isUpI = Input.GetKeyUp(KeyCode.I);

        if (isDownI)
        {
            bird.rightFinAngle = 1.0f;
        }
        if (isUpI)
        {
            bird.rightFinAngle = 0.0f;
        }

    }

    private void jBtnInput()
    {

        bool isDownJ = Input.GetKeyDown(KeyCode.J);
        bool isUpJ = Input.GetKeyUp(KeyCode.J);

        if (isDownJ)
        {
            bird.leftFinAngle = -1.0f;
        }
        if (isUpJ)
        {
            bird.leftFinAngle = 0.0f;
        }

    }

    private void kBtnInput()
    {

        bool isDownK = Input.GetKeyDown(KeyCode.K);
        bool isUpK = Input.GetKeyUp(KeyCode.K);

        if (isDownK)
        {
            bird.rightFinAngle = -1.0f;
        }
        if (isUpK)
        {
            bird.rightFinAngle = 0.0f;
        }

    }


}
