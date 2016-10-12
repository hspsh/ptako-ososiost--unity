using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HighScoreButton : MonoBehaviour {

	public void onClick ()
    {
        //Application.LoadLevel(2);
        SceneManager.LoadScene(2);
    }

}
