using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private Transform m_playerTransform; // Player Transform

	[SerializeField]
	[Range(0, 1)]
	private float m_xSmooth = 0.3f;

	[SerializeField]
	[Range(0, 1)]
	private float m_ySmooth = 0.5f;

	void Update ()
	{
		float new_x = Mathf.Lerp(this.transform.position.x, m_playerTransform.position.x, m_xSmooth);
		float new_y = Mathf.Lerp(this.transform.position.y, m_playerTransform.position.y, m_ySmooth);
		this.transform.position = new Vector3(new_x, new_y, -10);
	}
}
