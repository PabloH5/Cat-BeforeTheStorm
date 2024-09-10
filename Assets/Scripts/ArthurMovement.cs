using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArthurMovement : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float runSpeed;
    [SerializeField] private float radiusNear;
    [SerializeField] private float radiusTooNear;
    [SerializeField] private float radiusJump;
    
    [Header("Madness Variables")]
    
    public int madness = 4;
    [SerializeField] private float madnessSpeed = 10f;
    [SerializeField] private float rotationSpeed = 200f;
    private bool isMad = false;

    private GameObject pigeon;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (madness > 0)
        {
            CheckPigeonDistance();
        }
        else
        {
            GoCrazy();
        }
        
    }

    void CheckPigeonDistance()
    {
        pigeon = FindPigeon();
        if (pigeon)
        {
            float distanceToPigeon = CalculateDistance();
            UpdateAnimations(distanceToPigeon);
            MoveToPigeon(distanceToPigeon);
            if (distanceToPigeon > radiusNear)
            {
                SetAnimatorBools(false, false);
            }
        }
        else
        {
            SetAnimatorBools(false, false);
        }
    }

    GameObject FindPigeon()
    {
        return FindObjectOfType<PigeonMovement>()?.gameObject;
    }

    float CalculateDistance()
    {
        return Vector3.Distance(transform.position, pigeon.transform.position);
    }
    
    //Update the animations deppending of the distance btw the cat and pigeon
    void UpdateAnimations(float distanceToPigeon)
    {
        if (distanceToPigeon < radiusNear && distanceToPigeon >= radiusTooNear)
        {
            SetAnimatorBools(true, false);
        }
        else if (distanceToPigeon < radiusTooNear)
        {
            SetAnimatorBools(false, true); 
        }
        else if (distanceToPigeon > radiusNear)
        {
            SetAnimatorBools(false, false); 
        }
    }
    //Move the cat to random pigeon
    void MoveToPigeon(float distanceToPigeon)
    {
        if (distanceToPigeon <= radiusTooNear)
        {
            Vector2 direction = CalculateDirection(pigeon);

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); 
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1); 
            }

            Vector2 targetPosition = (Vector2)transform.position + (direction * (runSpeed * Time.deltaTime));
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, runSpeed * Time.deltaTime);
        }
    }

    void SetAnimatorBools(bool isNear, bool isTooNear)
    {
        animator.SetBool("isNear", isNear);
        animator.SetBool("isTooNear", isTooNear);
    }

    private Vector2 CalculateDirection(GameObject go)
    {
        Vector2 direction = ((Vector2)go.transform.position - (Vector2)gameObject.transform.position).normalized;
        return direction;
    }
    
    void GoCrazy()
    {
        SetAnimatorBools(false, false);
        isMad = true;
    
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        Vector3 newPosition = transform.position + randomDirection * (madnessSpeed * Time.deltaTime);

        Vector3 clampedPosition = ClampToScreen(newPosition);
        transform.position = clampedPosition;

        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    Vector3 ClampToScreen(Vector3 targetPosition)
    {
        Vector3 screenMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)); 
        Vector3 screenMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)); 

        float clampedX = Mathf.Clamp(targetPosition.x, screenMin.x, screenMax.x);
        float clampedY = Mathf.Clamp(targetPosition.y, screenMin.y, screenMax.y);

        return new Vector3(clampedX, clampedY, targetPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pigeon"))
        {
            madness--;
        }
    }
}
