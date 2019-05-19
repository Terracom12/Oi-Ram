using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
	[HideInInspector]
	public bool isGameOver;

	[SerializeField]
	private Transform m_player;

    void Update()
    {
		// If The player is below this object...
		if (m_player.position.y <= gameObject.transform.position.y)
		{
			// Set isGameOver to true and load the 'GameOver' scene
			isGameOver = true;
			SceneManager.LoadScene("GameOverScene");
		}
    }
}
