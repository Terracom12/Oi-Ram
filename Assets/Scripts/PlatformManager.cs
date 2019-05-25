using System.Collections;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    // To make sure that if the player jumps on twice, it doesn't count
    private bool m_scoreUpdated = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is the player (on the player layer)
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!m_scoreUpdated)
                // Increase the player's score
                GameOver.score++;
            m_scoreUpdated = true;
        }
    }
}
