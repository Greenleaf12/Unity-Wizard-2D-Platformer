using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.Find("Player");
        target = player.transform;
    }
  
    private void FixedUpdate()
    {
        Follow();      
    }

    void Follow()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = targetPosition;
        }
    }
}
