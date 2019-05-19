using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameOverMenu : MonoBehaviour
{
	public void RestartButton()
	{
		SceneManager.LoadScene("MainScene");
	}

    public void QuitButton()
	{
		if (EditorApplication.isPlaying)
			EditorApplication.isPlaying = false;
		else
			Application.Quit();
	}
}
