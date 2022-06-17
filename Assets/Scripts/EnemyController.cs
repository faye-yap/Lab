using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;
    private float originalX;
    private float maxOffset;
    private float enemyPatrolTime;
    private int moveRight;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        
        GameManager.OnPlayerDeath += EnemyRejoice;
        enemyPatrolTime = gameConstants.enemyPatrolTime;
        maxOffset = gameConstants.maxEnemyOffset;
        moveRight = -1;
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        ComputeVelocity();
    }

    void EnemyRejoice(){
        Debug.Log("Mario died :(");
    }

    void ComputeVelocity(){
        velocity = new Vector2((moveRight)*maxOffset/enemyPatrolTime,0);
    }
    
    void MoveEnemy(){
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player"){
            float yOffset = (other.transform.position.y - this.transform.position.y);
            if (yOffset > 0.75f){
                KillSelf();
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            damagePlayer();
        }
    }

    void  KillSelf(){
		// enemy dies
		CentralManager.centralManagerInstance.increaseScore();
		StartCoroutine(flatten());
		Debug.Log("Kill sequence ends");
	}

    IEnumerator  flatten(){
		Debug.Log("Flatten starts");
		int steps =  5;
		float stepper =  1.0f/(float) steps;

		for (int i =  0; i  <  steps; i  ++){
			this.transform.localScale  =  new  Vector3(this.transform.localScale.x, this.transform.localScale.y  -  stepper, this.transform.localScale.z);

			// make sure enemy is still above ground
			this.transform.position  =  new  Vector3(this.transform.position.x, gameConstants.groundSurface  , this.transform.position.z);
			yield  return  null;
		}
		Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		Debug.Log("Enemy returned to pool");
		yield  break;
	}

    public void damagePlayer(){
        CentralManager.centralManagerInstance.damagePlayer(gameConstants.enemyDamage);
    }

    
}
