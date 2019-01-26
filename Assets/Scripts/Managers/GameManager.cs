using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        UtilSound.instance.StopAllSounds();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ReturnToMainMenu();
        }
        else if (Input.GetButtonDown("Restart"))
        {
            Restart();
        }
	}

    public void ObjectEventCompleted()
    {
        Debug.Log("ObjectEventCompleted");
    }

    public void Restart()
    {
        //UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        //UtilSound.instance.PlaySound("click", 1.0f, false, true);
        SceneManager.LoadScene("MainMenu");
    }

}
