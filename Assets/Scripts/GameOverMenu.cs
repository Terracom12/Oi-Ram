using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_scoreText;

    void Awake()
    {
        m_scoreText.text = "Score: " + GameOver.score.ToString();
    }

	public void RestartButton()
	{
        GameOver.score = 0;
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
