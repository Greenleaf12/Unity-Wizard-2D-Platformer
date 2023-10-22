using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller;
	public Animator animator;
	public PlayerSwordFX PlayerSwordSwing;

	private StaminaBar staminabar;
	public ParticleSystem particleprefab;
	public ParticleSystem staffexplode;
	public ParticleSystem runningEffect;

	public Transform firePoint;
	public float runSpeed = 40f;

	float horizontalMove = 0f;
	public bool jump = false;
	bool crouch = false;

	public bool running = false;
	public bool walking = false;
	public bool attacking = false;
	public bool jumping = false;

	public float cooldownTime = 1f;
	private float nextFireTime = 0f;

	private float staminaTotal;

	public ParticleSystem FXSpawn;

	public void Awake()
	{
		staminabar = FindObjectOfType<StaminaBar>();
		GameObject.Instantiate(FXSpawn, transform.position, Quaternion.identity);		
	}

	public void Update()
	{
		staminaTotal = staminabar.staminaAmount;
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		// Jumping //
		if (Input.GetButtonDown("Jump") && staminaTotal > 10f)
		{
			jump = true;
			jumping = true;
			FindObjectOfType<AudioManager>().Stop("Run");

			animator.SetBool("IsJumping", true);
		}
		else if (Input.GetButtonUp("Jump"))
		{
			jumping = false;		
		}

		// Crouching //
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		// Running //
		if (Input.GetButton("Run"))
		{
			if (staminaTotal > 10f)
			{
				running = true;
				runSpeed = 80f;			
			}
			else
			{
				runningEffect.Stop();
				running = false;
				runSpeed = 40f;			
			}
		}

		if (Input.GetButtonDown("Run") && staminaTotal > 5f)
		{
			FindObjectOfType<AudioManager>().Play("Magic_Run");
			runningEffect.Play();
			animator.speed = 2.0f;
		}
		else if (Input.GetButtonUp("Run"))
		{
			runSpeed = 40f;
			FindObjectOfType<AudioManager>().Stop("Magic_Run");
			runningEffect.Stop();
			animator.speed = 1.0f;
		}

		// Sword Attack // 
		if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
		{
			Sword();
			nextFireTime = Time.time + cooldownTime/4;
		}

		// Staff Attack //
		if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
		{
			//attacking = true;
			nextFireTime = Time.time + cooldownTime / 5;
		}

		if (Input.GetKeyUp(KeyCode.Space))
		{
			attacking = false;
		}

		// Running Sound //
		if (Input.GetButtonDown("Horizontal"))
		{
			FindObjectOfType<AudioManager>().Play("Run");
			walking = true;
		}
		else if (Input.GetButtonUp("Horizontal"))
		{
			FindObjectOfType<AudioManager>().Stop("Run");
			walking = false;
		}
	}

	public void Sword()
	{
		animator.SetTrigger("Sword");
		PlayerSwordSwing.PlayClip();		
	}

	public void Kick()
	{
		attacking = true;
		animator.SetTrigger("Kick");
		staffexplode.Play();
		Instantiate(particleprefab, firePoint.position, firePoint.rotation);
		FindObjectOfType<AudioManager>().Play("Staff_Fire");
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Frog")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}

		if (collision.gameObject.tag == "MovingPlatform")
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Coins"))
		{
			Destroy(other.gameObject);
		}
	}

	public void OnLanding()
	{
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool isCrouching)
	{
		animator.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}