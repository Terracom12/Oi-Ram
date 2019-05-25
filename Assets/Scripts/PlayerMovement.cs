using UnityEngine;

// Make sure that any object using this script has a Rigidbody2D and an Animator
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{

	[Header("Movement:")]
	[SerializeField]
	private float m_moveVelocity = 12f;
	[SerializeField]
	private float m_maxSpeed = 15f;

	[Header("Jumping:")]
	[SerializeField]
	private float m_jumpVelocity = 15f;
	[SerializeField]
	[Tooltip("The amount that gravity will be multiplied by to increase fall speed.")]
	private float m_fallMultiplier = 6f;
	[SerializeField]
	[Tooltip("The amount that gravity will be multiplied by when the player JUST taps the jump button.")]
	private float m_lowJumpMultiplier = 2f;
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
	[Tooltip("A group of layer(s) that represents what the player can move on top of.")]
	private LayerMask m_whatIsGround;

	private Rigidbody2D m_rb;
    private Animator m_anim;

	private float m_horizontal;
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
        m_anim = GetComponent<Animator>();

    }

	// Always do input in the Update method
	void Update()
	{
		m_grounded = Physics2D.Linecast(transform.position, m_groundCheck.position, m_whatIsGround);

        if (m_grounded)
        {
            m_canJump = true;
            m_lastGrounded = Time.time;
        }
            

		m_horizontal = Input.GetAxis("Horizontal");

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
        /********************HORIZONTAL MOVEMENT********************/
        if (m_horizontal * GetComponent<Rigidbody2D>().velocity.x < m_maxSpeed)
			m_rb.velocity = new Vector2(m_horizontal * m_moveVelocity, m_rb.velocity.y);

		if (Mathf.Abs(m_rb.velocity.x) > m_maxSpeed)
			m_rb.velocity = new Vector2(Mathf.Sign(m_rb.velocity.x) * m_maxSpeed, m_rb.velocity.y);
        /********************HORIZONTAL MOVEMENT********************/

        /********************ANIMATION********************/
        if (m_grounded)
        {
            m_anim.SetFloat("speed", Mathf.Abs(m_rb.velocity.x) / 5);
            m_anim.SetBool("jumping", false);
        }
        else
            m_anim.SetFloat("speed", 0);

        if(m_jumping)
            m_anim.SetBool("jumping", true);
        /********************ANIMATION********************/

        /********************JUMPING********************/
        if (m_jumping)
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

		/********************TURNING********************/
        // If the player should be facing right and isn't, and vica versa
		if (m_horizontal > 0 && !m_facingRight || m_horizontal < 0 && m_facingRight)
		{
            // Reverse whether the player is facing right
			m_facingRight = !m_facingRight;
            // Multiply the x-scale by -1
			transform.localScale = 
                new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
		}
        /********************TURNING********************/
    }
}
