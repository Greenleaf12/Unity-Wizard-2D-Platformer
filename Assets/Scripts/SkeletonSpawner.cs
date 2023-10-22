using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkeletonSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public ParticleSystem particlePrefab;
    public ParticleSystem particlePrefab_2;
    public ParticleSystem particlePrefab_3;
    public int EnemyCount;

    public float cooldownTime = 200.0f;
    private float nextFireTime = 0f;

    private AudioSource m_MyAudioSource;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time > nextFireTime && EnemyCount > 0)
            {
                nextFireTime = Time.time + cooldownTime;
                m_MyAudioSource.Play();
                Instantiate(enemyPrefab, transform.position, Quaternion.identity);
                Instantiate(particlePrefab, transform.position, Quaternion.identity);
                Instantiate(particlePrefab_2, transform.position, Quaternion.identity);

                EnemyCount -= 1;

                if (EnemyCount == 0)
                {
                    Destroy();
                }
            }
        }
    }

    public void Destroy()

    {
        FindObjectOfType<AudioManager>().Play("Teleporter");
        Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Instantiate(particlePrefab_2, transform.position, Quaternion.identity);
        Instantiate(particlePrefab_3, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}