using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PigeonMovement : MonoBehaviour
{
    public float speed;
    private Vector2 direction;
    
    public float mouseSpeed = 7f; //  speed for move away of the mouse
    public float radius = 2f; //minimal distance for move away of the mouse

    
    void Start()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Limits"))
        {
            Destroy(this.gameObject);
        }
    }
}
