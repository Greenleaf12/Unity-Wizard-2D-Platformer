using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask Player;
    public Collider2D triggerSkel;
    public Rigidbody2D rbSkel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

// void OnTriggerStay2D(Collider2D triggerSkel)
// {
//     if (triggerSkel.gameObject.tag == "Player")
//
//     {
//            attackMode = true;
//            rbSkel.velocity = new Vector2(-7, 0);
//     }
//
// }

    // void OnTriggerExit2D(Collider2D triggerSkel)
    //
    // {
    //
    //     StopAttack();
    //     Move();
    //    
    // }
}
