using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consummablePrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox;
    private bool hit = false;
    private Vector3 startPos;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.GetComponent<Rigidbody2D>();
        startPos = rigidBody.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player") && !hit){
            hit = true;
            rigidBody.AddForce(new Vector2(0,rigidBody.mass*20), ForceMode2D.Impulse);
            Instantiate(consummablePrefab, new Vector3(this.transform.position.x,this.transform.position.y + 1.0f, this.transform.position.z),Quaternion.identity);
            StartCoroutine(DisableHittable());
            
        }

        /*if (other.gameObject.CompareTag("Obstacle") && other.contacts[0].normal == Vector2.down) {
            
            rigidBody.transform.position = startPos;
            rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            spriteRenderer.sprite = usedQuestionBox;
            springJoint.enabled = false;
            
            

        }*/

        


        

        

        
    }

    bool ObjectMovedAndStopped(){
        return Mathf.Abs(rigidBody.velocity.magnitude)<0.01;
    }

    IEnumerator DisableHittable(){
        if (!ObjectMovedAndStopped()){
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        spriteRenderer .sprite = usedQuestionBox;
        rigidBody.bodyType = RigidbodyType2D.Static;

        this.transform.localPosition = Vector3.zero;
        springJoint.enabled = false;
    }
}
