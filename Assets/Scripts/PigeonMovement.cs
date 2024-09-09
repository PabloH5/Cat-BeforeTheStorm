using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    
    public float mouseSpeed = 7f; //  speed for move away of the mouse
    public float radius = 2f; //minimal distance for move away of the mouse
    
    public float verticalSpeed = 0.5f; // speed vertical movement
    public float amplitude = 0.5f; // amplitude of the vertical movement

    private Vector2 startPosition;
    private float timeCounter = 0f;
    
    public float Speed
    {
        get;
        set;
    }
    
    void Start()
    {
        startPosition = transform.position;
        //depending on the site where spawn change the direction 
        if (transform.position.x < 0) 
        {
            direction = Vector2.right;
            
        }
        else 
        {
            direction = Vector2.left;
        }
        
        transform.localScale = new Vector3(-direction.x, 1, 1);
    }

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pigeonPosition = transform.position;
        float distanceToMouse = Vector2.Distance(pigeonPosition, mousePosition);

        
        if (distanceToMouse < radius)
        {
            
            Vector2 fleeDirection = (pigeonPosition - (Vector2)mousePosition).normalized;
            transform.Translate(fleeDirection * (mouseSpeed * Time.deltaTime));
        }
        else
        {
            
            transform.Translate(direction * (speed * Time.deltaTime));
        }
        
    }
}
