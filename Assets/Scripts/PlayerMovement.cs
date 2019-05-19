using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

	[Header("Movement:")]
	[SerializeField]
	private float m_moveForce = 365f;
	[SerializeField]
	private float m_maxSpeed = 5f;

	[Header("Jumping:")]
	[SerializeField]
	private float m_jumpForce = 450f;
	[SerializeField]
	[Tooltip("The time that the player has to recover after falling off a platform. (Milliseconds)")]
	[Range(50, 1000)]
	private float m_recoveryTime = 250f;

	[Header("Checks:")]
	[SerializeField]
	[Tooltip("An empty game object's transform for where the player should collide with the ground")]
	private Transform m_groundCheck;
	[SerializeField]
	[Tooltip("An empty game object's transform for where the player should collide with the ceiling")]
	private Transform m_ceilingCheck;

	[SerializeField]
	[Tooltip("A group of layer(s) that represents what the player can not collide with.")]
	private LayerMask m_whatIsNotGround;

	private Rigidbody2D m_rb;

	private float m_horizontal, m_vertical;
	private float m_lastGrounded;

	private bool m_facingRight = true;
	private bool m_jumping = false;
	private bool m_canJump = false;
	private bool m_grounded = false;

	// Use this for initialization
	void Start()
	{
		m_rb = GetComponent<Rigidbody2D>();
	}


	void Update()
	{
		// Bitshift 1 the value of the LayerMask
		// Then reverse (~) it because we want to
		// INCLUDE everything that is ground.
		m_grounded = Physics2D.Linecast(transform.position, m_groundCheck.position, 1 << m_whatIsNotGround.value);

		if (m_grounded)
		{
			m_canJump = true;
			m_lastGrounded = Time.time;
		}

		m_horizontal = Input.GetAxis("Horizontal");

		m_vertical = Input.GetAxis("Vertical");
		if (m_canJump)
		{
			if (Input.GetButtonDown("Jump") && m_grounded)
				m_jumping = true;
			else if (Input.GetButtonDown("Jump") && (Time.time - m_lastGrounded) <= (m_recoveryTime / 1000))
				m_jumping = true;
		}
	}

	void FixedUpdate()
	{
		if (m_horizontal * GetComponent<Rigidbody2D>().velocity.x < m_maxSpeed)
			m_rb.AddForce(m_horizontal * Vector2.right * m_moveForce);

		if (Mathf.Abs(m_rb.velocity.x) > m_maxSpeed)
			m_rb.velocity = new Vector2(Mathf.Sign(m_rb.velocity.x) * m_maxSpeed, m_rb.velocity.y);

		if(m_jumping)
		{
			m_rb.AddForce(new Vector2(0f, m_jumpForce));
			m_jumping = false;
			m_canJump = false;
		}

		if (m_horizontal > 0 && !m_facingRight)
		{
			m_facingRight = true;
			gameObject.transform.localScale = new Vector3(1, 1, 1);
		}
		else if (m_horizontal < 0 && m_facingRight)
		{
			m_facingRight = false;
			gameObject.transform.localScale = new Vector3(-1, 1, 1);
		}
	}
}
