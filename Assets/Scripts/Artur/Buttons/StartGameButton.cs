using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGameButton : MonoBehaviour {

    public void onClick()
    {
        SceneManager.LoadScene(1);
    }

}
