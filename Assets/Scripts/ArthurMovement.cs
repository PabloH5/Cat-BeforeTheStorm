using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ArthurMovement : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private float runSpeed;
    [SerializeField] private float radiusNear;
    [SerializeField] private float radiusTooNear;
    [SerializeField] private float radiusJump;

    private GameObject pigeon;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPigeonDistance();

        // if (pigeon == null)
        // {
        //     animator.SetBool("isNear", false);
        //     animator.SetBool("isTooNear", false);
        //     animator.SetBool("canJump", false);
        // }
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

    // Método para encontrar la paloma
    GameObject FindPigeon()
    {
        return FindObjectOfType<PigeonMovement>()?.gameObject;
    }

    // Método para calcular la distancia a la paloma
    float CalculateDistance()
    {
        return Vector3.Distance(transform.position, pigeon.transform.position);
    }

    // Método para actualizar las animaciones según la distancia
    void UpdateAnimations(float distanceToPigeon)
    {
        Debug.Log(distanceToPigeon);
        if (distanceToPigeon < radiusNear && distanceToPigeon >= radiusTooNear)
        {
            SetAnimatorBools(true, false); // `isNear == true`, `isTooNear == false`, `canJump == false`
        }
        else if (distanceToPigeon < radiusTooNear)
        {
            SetAnimatorBools(false, true); // `isNear == false`, `isTooNear == true`, `canJump == false`
        }
        // else if (distanceToPigeon < radiusJump)
        // {
        //     SetAnimatorBools(false, false); // `isNear == false`, `isTooNear == false`, `canJump == true`
        // }
        else if (distanceToPigeon > radiusNear)
        {
            SetAnimatorBools(false, false); // Vuelve al estado `Sleeping`
        }
    }


    // Método para mover a Arthur hacia la paloma si está dentro del rango "TooNear"
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

    // Método para actualizar los booleanos del Animator
    void SetAnimatorBools(bool isNear, bool isTooNear)
    {
        animator.SetBool("isNear", isNear);
        animator.SetBool("isTooNear", isTooNear);
        // animator.SetBool("canJump", canJump);
    }

    // Método para calcular la dirección hacia la paloma
    private Vector2 CalculateDirection(GameObject go)
    {
        Vector2 direction = ((Vector2)go.transform.position - (Vector2)gameObject.transform.position).normalized;
        return direction;
    }
}
