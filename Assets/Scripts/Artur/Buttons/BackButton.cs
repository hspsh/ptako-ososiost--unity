using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackButton : MonoBehaviour {

	public void onClick()
    {
        SceneManager.LoadScene(0);
    }

}
