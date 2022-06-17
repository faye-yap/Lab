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

    public Vector2 movement;
    public bool jumpInput;
    public bool started;
    private bool faceRightState;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    private ParticleSystem dustCloud;
    private Material baseMat;
    public Material glowMat;

   
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        startingPos = marioBody.transform.position;
        menuController = GameObject.FindGameObjectWithTag("UI").GetComponent<MenuController>();
        maxHP = health;
        marioAnimator = GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
        onGroundState = true;
        dustCloud = GameObject.FindGameObjectWithTag("Particle").GetComponent<ParticleSystem>();
        baseMat = marioSprite.material;
        GameManager.OnPlayerDeath += PlayerDiesSequence;
        
        
    }

    
    void PlayerDiesSequence() {
        Debug.Log("mario ded");
    }
    
    void FixedUpdate() {

        
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKey("a") || Input.GetKey("d")){
            marioSprite.material = glowMat;
            if (Mathf.Abs(moveHorizontal) > 0){
                Vector2 movement = new Vector2(moveHorizontal, 0);
                marioBody.AddForce(movement * acceleration);
                if (marioBody.velocity.x > maxHorizontalSpeed){
                    marioBody.velocity = new Vector2(maxHorizontalSpeed,marioBody.velocity.y);

                } else if (marioBody.velocity.x < -maxHorizontalSpeed){
                    marioBody.velocity = new Vector2(-maxHorizontalSpeed,marioBody.velocity.y);
                }
            }


        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // stop
            if(onGroundState){
                marioSprite.material = baseMat;
            }
            marioBody.velocity = new Vector2(marioBody.velocity.x/5,marioBody.velocity.y);
            marioAnimator.SetTrigger("onSkid");

        }

        if (Input.GetKeyDown("space") && onGroundState){
            marioSprite.material = glowMat;
            onGroundState = false;
            marioAnimator.SetBool("onGround",onGroundState);
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            if(marioBody.velocity.y > maxUpSpeed){
                marioBody.velocity = new Vector2(marioBody.velocity.x,maxUpSpeed);
            }
            
            countScoreState = true;
            
        }
        

        
          
       
        
       
    }

    void OnCollisionEnter2D(Collision2D c) {
        if (c.gameObject.CompareTag("Ground") || (c.gameObject.CompareTag("Pipe") && c.contacts[0].normal == Vector2.up)) {
            marioSprite.material = baseMat;
            onGroundState = true;
            marioAnimator.SetBool("onGround",onGroundState);
            countScoreState = false;
            
            dustCloud.Play();
        }

        if (c.gameObject.CompareTag("Enemy")) {
            if (c.contacts[0].normal == Vector2.up){
                marioBody.AddForce(Vector2.up * upSpeed,ForceMode2D.Impulse);
        
                if (marioBody.velocity.y > maxUpSpeed){
                    marioBody.velocity = new Vector2(marioBody.velocity.x,maxUpSpeed);
                }
            }
        }

        if (c.gameObject.CompareTag("Obstacle")) {
            onGroundState = true;
            marioAnimator.SetBool("onGround",onGroundState);
            
        }

        

    }

    

    // Update is called once per frame
    void Update()
    {   
        marioAnimator.SetFloat("xSpeed",Mathf.Abs(marioBody.velocity.x));
        
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
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

    void PlayJumpSound(){
        marioAudio.PlayOneShot(marioAudio.clip);
    }

    
    
}
