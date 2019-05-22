using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [HideInInspector]
    static public uint score = 0;

	[SerializeField]
	private Transform m_player;

    void Update()
    {
		// If The player is below this object...
		if (m_player.position.y <= gameObject.transform.position.y)
		{
			// Load the 'GameOver' scene
			SceneManager.LoadScene("GameOverScene");
		}
    }
}
