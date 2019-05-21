using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platforms:")]
    [SerializeField]
    [Tooltip("The platform prefab to be used.")]
    private GameObject m_platform;
    [SerializeField]
    [Tooltip("The number of platforms to be spawned.")]
    private uint m_numPlatforms = 10;

    [Header("Offsets:")]
    [SerializeField]
    [Tooltip("The max y offset, either above or below.")]
    private float m_maxYOffset = 6f;
    [SerializeField]
    [Tooltip("The x offset of each platform.")]
    private float m_xOffset = 10f;

    void Start()
    {
        float platformX = 0, platformY = 0;
        Vector2 platformPosition = new Vector2(platformX, platformY);
        
        // Create a new platform at (0, 0) with (0, 0, 0) rotation and this object as a parent.
        Instantiate(m_platform, platformPosition, Quaternion.identity, transform);

        for (int i = 0; i < m_numPlatforms-1; i++)
        {
            platformY = Random.Range(platformY - m_maxYOffset, platformY + m_maxYOffset);
            platformX += m_xOffset;
            platformPosition = new Vector2(platformX, platformY);

            Instantiate(m_platform, platformPosition, Quaternion.identity, transform);
        }
    }
}
