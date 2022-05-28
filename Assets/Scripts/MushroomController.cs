using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{

    private Rigidbody2D mushroomBody;
    private float direction; //-1 for left, 1 for right
    public float xVelocity;
    public float initialYVelocity;
    private bool moving = true;
    
    
    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        int random = Random.Range(0,2);
        
        direction -= 0.5f;
        direction *= 2;
        mushroomBody.velocity = new Vector2(direction * xVelocity, initialYVelocity);
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(moving){
            
            Vector2 nextPos = mushroomBody.transform.position + new Vector3(direction * xVelocity * Time.fixedDeltaTime,mushroomBody.velocity.y * Time.fixedDeltaTime,0);
            mushroomBody.MovePosition(nextPos);
            
            
        }
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("Pipe")) {
            mushroomBody.velocity = new Vector2(-direction * xVelocity,mushroomBody.velocity.y);
            direction *= -1;


        }

        if (other.gameObject.CompareTag("Player")){
            mushroomBody.velocity = Vector2.zero;
            moving = false;
            
        }

    }

    private void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
