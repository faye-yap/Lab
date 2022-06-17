using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public Text score;
    private int playerScore = 0;
    private int playerHealth = 100;

    public void increaseScore(){
        playerScore += 1;
        score.text = "Score: " + playerScore.ToString();
        IncreaseScore();
    }

    public void damagePlayer(int damage){
        playerHealth -= damage;
        if (playerHealth == 0){
            OnPlayerDeath();
        }
    }
    
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent IncreaseScore;

}
