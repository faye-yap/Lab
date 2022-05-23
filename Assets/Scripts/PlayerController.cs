using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxHorizontalSpeed;
    public float upSpeed;

    public float maxUpSpeed;
    private Rigidbody2D marioBody;
    private bool onGroundState = true;
    private SpriteRenderer marioSprite;

    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;

    public float health = 100.0f;
    public float maxHP;

    private MenuController menuController;
    private EnemyController enemyController;
    private Vector3 startingPos;
    

   
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        startingPos = marioBody.transform.position;
        menuController = GameObject.FindGameObjectWithTag("UI").GetComponent<MenuController>();
        maxHP = health;
        
    }

    void Jump(){
        marioBody.AddForce(Vector2.up * upSpeed,ForceMode2D.Impulse);
        
        if (marioBody.velocity.y > maxUpSpeed){
            marioBody.velocity = new Vector2(marioBody.velocity.x,maxUpSpeed);
        }
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
    
        if(Mathf.Abs(moveHorizontal)>0){
            
            Vector2 movement = new Vector2(moveHorizontal,0);
            if (Input.GetKey("a") || Input.GetKey("d")){
                if (Mathf.Abs(marioBody.velocity.x) < maxHorizontalSpeed){
                    marioBody.AddForce(movement*acceleration*marioBody.mass);
                    //print("go");
                } else {

                    if (moveHorizontal > 0){
                        marioBody.velocity = new Vector2(maxHorizontalSpeed,marioBody.velocity.y);
                    } else if (moveHorizontal < 0){
                        marioBody.velocity = new Vector2(-maxHorizontalSpeed,marioBody.velocity.y);
                    }
                    
                }
            }
        }
        
        
        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            
                marioBody.velocity = new Vector2(0,marioBody.velocity.y);
                //print("stop");
            
            
        }
        

        if (Input.GetKeyDown("space")&&onGroundState){
            onGroundState = false;
            Jump();
            countScoreState = true;
        }
          
       
        
       
    }

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.CompareTag("Ground")) {
            onGroundState = true;
            countScoreState = false;
            scoreText.text = "Score: " + score.ToString();
        }

        if (c.gameObject.CompareTag("Enemy")) {
            if (c.contacts[0].normal == Vector2.up){
                Jump();
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")){
            //Debug.Log("Got hit lol");
            health -= 34;
            Debug.Log(health);
            menuController.updateHPBar();
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(marioBody.transform.rotation.z != 0){
            marioBody.transform.rotation = new Quaternion(0,0,0,0);
        }

        if (Input.GetKeyDown("a")){
            marioSprite.flipX = true;

        } else if (Input.GetKeyDown("d")){
            marioSprite.flipX = false;
        }

        if (!onGroundState && countScoreState){
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f){
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }

        
    }

    public void resetMario() {
        marioBody.transform.position = startingPos;
        marioBody.velocity = Vector2.zero;
        score = 0;
    }
    
}
