using System.Collections;
using UnityEngine;

public class PlatformScore : MonoBehaviour
{
    // To make sure that if the player jumps on twice, it doesn't count
    private bool m_scoreUpdated = false;

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(!m_scoreUpdated)
            // Increase the player's score
            GameOver.score++;
        m_scoreUpdated = true;
    }
}
