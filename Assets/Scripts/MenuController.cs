using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{

    private PlayerController playerController;
    private EnemyController enemyController;
    private Transform currentHP;
    private float hpPercent;
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyController = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyController>();
        
    }

    public void StartButtonClicked(){
        foreach (Transform child in transform){
            if (child.name == "CurrentHP"){
                    currentHP = child;
                    
                }
            if (child.name != "ScoreText" && child.name != "CurrentHP" && child.name != "HPOutline"){
                Debug.Log("Child found: " + child.name);
                child.gameObject.SetActive(false);
            }
            
        }
        currentHP.transform.localScale = new Vector3(1,1,1);
        playerController.scoreText.text = "Score: 0";        
        playerController.started = true;
        Time.timeScale = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        
        

        if (playerController.health <= 0){
            
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }

    public void showOverlay(){
        foreach (Transform child in transform){
            if (child.name != "ScoreText"){
                //Debug.Log("Child found: " + child.name);
                child.gameObject.SetActive(true);
            
            }
            
        }
    }

    public void updateHPBar() {
        
        hpPercent = playerController.health/playerController.maxHP;
        if (hpPercent<0) hpPercent = 0;
        currentHP.transform.localScale = new Vector3(hpPercent,1,1);
        //Debug.Log("Current HP: " + playerController.health.ToString());
        //Debug.Log("Max HP: " + playerController.maxHP.ToString());
    }
}
