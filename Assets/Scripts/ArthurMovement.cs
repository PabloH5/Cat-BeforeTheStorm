using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ArthurMovement : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float radiusNear;
    [SerializeField] private float radiusTooNear;
    [SerializeField] private float radiusJump;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPigeonDistance();
    }

    void CheckPigeonDistance()
    {
        GameObject pigeon = FindObjectOfType<PigeonMovement>()?.gameObject;
        if (pigeon != null)
        {
            float distanceToPigeon = Vector3.Distance(transform.position, pigeon.transform.position);
            // Debug.Log("Distancia a la paloma: " + distanceToPigeon);

            if (distanceToPigeon <= radiusNear && distanceToPigeon > radiusTooNear)
            {
                Debug.Log("Paloma está cerca pero no demasiado cerca.");
                animator.SetBool("isNear", true);
                animator.SetBool("isTooNear", false);
                animator.SetBool("canJump", false);
            }
            else if (distanceToPigeon <= radiusTooNear && distanceToPigeon > radiusJump)
            {
                Debug.Log("Paloma está demasiado cerca.");
                animator.SetBool("isTooNear", true);
                animator.SetBool("canJump", false);
            }
            else if (distanceToPigeon <= radiusJump)
            {
                Debug.Log("Paloma puede saltar.");
                animator.SetBool("isTooNear", true);
                animator.SetBool("canJump", true);
            }

            pigeon = null;
        }
    }

}
