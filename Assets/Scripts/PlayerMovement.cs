using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	private bool m_facingRight = true;
	private bool m_jump = false;
	[SerializeField]
	private float m_moveForce = 365f;
	[SerializeField]
	private float m_maxSpeed = 5f;
	[SerializeField]
	private float m_jumpForce = 1000f;

	[SerializeField]
	private Transform m_groundCheck;
	[SerializeField]
	private Transform m_ceilingCheck;

	private bool m_grounded = false;
	private Rigidbody2D m_rb;

	private float m_horizontal, m_vertical;

	// Use this for initialization
	void Start ()
	{
		m_rb = GetComponent<Rigidbody2D>();
	}


	void Update()
	{
		m_grounded = Physics2D.Linecast(transform.position, m_groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		m_horizontal = Input.GetAxisRaw("Horizontal");

		m_vertical = Input.GetAxisRaw("Vertical");

		if (Input.GetButtonDown("Jump") && m_grounded)
			m_jump = true;
	}

	void PhysicsUpdate ()
	{
		
	}
}
