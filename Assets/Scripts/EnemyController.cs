using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyController : MonoBehaviour
{

    private float originalX;
    private float maxOffset = 5.0f;
    private float enemyPatrolTime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*maxOffset/enemyPatrolTime,0);
    }
    
    void MoveEnemy(){
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    public void resetEnemy(){
        enemyBody.transform.position = new Vector3(originalX,transform.position.y,transform.position.z);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset){
            MoveEnemy();
        } else {
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        }
        
    }

    
}
