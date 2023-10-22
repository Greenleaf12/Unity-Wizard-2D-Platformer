using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

// Telekinesis ability
public class TeleKi : MonoBehaviour
{
    Vector2 difference = Vector2.zero;
    Vector2 currentVelocity;
    public float moveSpeed = 10;
    public float smoothTime = 0.3f;
    public float rotateSpeed = 2;
    public ParticleSystem telekEffect;
    public ParticleSystem playerTelekEffect;
    public UnityEngine.Rendering.Universal.Light2D Light;
    private Animator animator;

    private Transform Target;
    private static ParticleSystem.Particle[] particles = new ParticleSystem.Particle[500];
    int count;
    bool isTeleActive = false;

    private Collider2D Collider;

    public GameObject smasherObj;

    private void Awake()
    {
        //Target = this.transform;
        GameObject player = GameObject.FindWithTag("Player");
        animator = player.GetComponent<Animator>();

        GameObject pFX = GameObject.FindWithTag("PFX");
        playerTelekEffect = pFX.GetComponent<ParticleSystem>();
        Collider = GetComponent<Collider2D>();
        Collider.enabled = true;       
    }

    private void OnMouseDown()
    {
        if (gameObject != null)
        {
            FindObjectOfType<AudioManager>().Play("TeleK");
            FindObjectOfType<AudioManager>().SetVolume("TeleK", 0.6f);
            animator.SetBool("IsTeleK", true);
            smasherObj.SetActive(false);
        }
    }

    private void OnMouseDrag()
    {
        if (gameObject !=null)
        {
            isTeleActive = true;
            Target = this.transform;
            transform.position = Vector2.MoveTowards(transform.position, difference, moveSpeed * Time.deltaTime);
            //transform.position = Vector2.SmoothDamp(transform.position, difference, ref currentVelocity, smoothTime, moveSpeed);
            transform.Rotate(0.0f, 0.0f, rotateSpeed, Space.Self);
            telekEffect.Play();
            playerTelekEffect.Play();
            Light.intensity = 2.0f;
            Collider.enabled = false;
        }     
    }

    private void OnMouseUp()
    {
        FindObjectOfType<AudioManager>().Fade("TeleK");
        isTeleActive = false;
        Target = null;
        telekEffect.Stop();
        playerTelekEffect.Stop();
        Light.intensity = 0.0f;
        animator.SetBool("IsTeleK", false);
        Collider.enabled = true;
        smasherObj.SetActive(true);
    }

    void Update()
    {      
        if (gameObject != null && playerTelekEffect != null )
        {
            difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);// - (Vector2)transform.position;
            count = playerTelekEffect.GetParticles(particles);

            for (int i = 0; i < count; i++)
            {
                if (Target != null)
                {
                    ParticleSystem.Particle particle = particles[i];

                    Vector2 v1 = playerTelekEffect.transform.TransformPoint(particle.position);
                    Vector2 v2 = Target.transform.position;

                    Vector2 tarPosi = (v2 - v1) * (particle.remainingLifetime / particle.startLifetime);
                    particle.position = playerTelekEffect.transform.InverseTransformPoint(v2 - tarPosi);
                    particles[i] = particle;
                }
            }
            playerTelekEffect.SetParticles(particles, count);
        }    
    }

    public void Deactivate()
    {
            FindObjectOfType<AudioManager>().Fade("TeleK");
            isTeleActive = false;
            Target = null;
            telekEffect.Stop();
            playerTelekEffect.Stop();
            Light.intensity = 0.0f;
            animator.SetBool("IsTeleK", false);
            Collider.enabled = true;
    }
}