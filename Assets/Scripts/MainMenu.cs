using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void PlayButton()
	{
		// Load the next scene
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OptionsButton()
	{
		
	}

	public void ExitButton()
	{
		if (EditorApplication.isPlaying)
			EditorApplication.isPlaying = false;
		else
			Application.Quit();
	}
}
