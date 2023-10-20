using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	public float downForce = -50;
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .16f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsGround2;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	public ParticleSystem jumpexplode;
	const float k_GroundedRadius = 0.6f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	public bool FacingRight = true;
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	public StaminaBar staminabar;

	private void Awake()
	{
		staminabar = FindObjectOfType<StaminaBar>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// Ground check
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded && m_Rigidbody2D.velocity.y < 0)
					OnLandEvent.Invoke();
			}
		}
		// Ground check 2
		Collider2D[] colliders2 = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround2);
		for (int i = 0; i < colliders2.Length; i++)
		{
			if (colliders2[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded && m_Rigidbody2D.velocity.y < 0)
					OnLandEvent.Invoke();
			}
		}
	}
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Platform")
        {
			m_AirControl = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, downForce));
		}
	}
	public void OnCollisionExit2D(Collision2D collision)
	{
		m_AirControl = true;
		
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);

			// Movement smoothing
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// Flip player 
			if (move > 0 && !m_FacingRight)
			{
				FacingRight = true;
				Flip();
			}
			
			else if (move < 0 && m_FacingRight)
			{
				FacingRight = false;
				Flip();
			}
		}

		// Jumping
		if (m_Grounded && jump)
		{
			// Add vertical force
			StartCoroutine(Jumping());
			m_Grounded = false;
			jumpexplode.Play();
			staminabar.RemoveStamina(10);
			FindObjectOfType<AudioManager>().Play("Jump");
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

			IEnumerator Jumping()
			{
				yield return new WaitForSeconds(0.6f);
				m_Rigidbody2D.AddForce(new Vector2(0f, downForce));
			}
		}
		
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;
		transform.Rotate(0f, 180f, 0f);		
	}
}
