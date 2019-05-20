using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{

	[Header("Movement:")]
	[SerializeField]
	private float m_moveVelocity = 4f;
	[SerializeField]
	private float m_maxSpeed = 8f;

	[Header("Jumping:")]
	[SerializeField]
	private float m_jumpVelocity = 17f;
	[SerializeField]
	[Tooltip("The amount that gravity will be multiplied by to increase fall speed.")]
	private float m_fallMultiplier = 10f;
	[SerializeField]
	[Tooltip("The amount that gravity will be multiplied by when the player JUST taps the jump button.")]
	private float m_lowJumpMultiplier = 8f;
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
	private bool m_jumpButton = false;
	private bool m_grounded = false;

	// Use this for initialization
	void Awake()
	{
		m_rb = GetComponent<Rigidbody2D>();
	}

	// Always do input in the Update method
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

		m_jumpButton = Input.GetButton("Jump");

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
			m_rb.velocity = new Vector2(m_horizontal * m_moveVelocity, m_rb.velocity.y);

		if (Mathf.Abs(m_rb.velocity.x) > m_maxSpeed)
			m_rb.velocity = new Vector2(Mathf.Sign(m_rb.velocity.x) * m_maxSpeed, m_rb.velocity.y);

		/********************JUMPING********************/
		if(m_jumping)
		{
			m_rb.velocity = new Vector2(m_rb.velocity.x, m_jumpVelocity);
			m_jumping = false;
			m_canJump = false;
		}
		// If the player is falling
		if (m_rb.velocity.y < 0)
		{
			// Then multiply the amount of gravity applied to the player, to fall faster
			m_rb.velocity += Vector2.up * Physics2D.gravity.y * (m_fallMultiplier - 1) * Time.deltaTime;
		}
		// If the player is going up and the jump button isn't pressed
		else if (m_rb.velocity.y > 0 && !m_jumpButton)
		{
			// Then make the player fall faster with low jump multiplier
			m_rb.velocity += Vector2.up * Physics2D.gravity.y * (m_lowJumpMultiplier - 1) * Time.deltaTime;
		}
		/********************JUMPING********************/

		/********************HORIZONTAL MOVEMENT********************/
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
		/********************HORIZONTAL MOVEMENT********************/
	}
}
