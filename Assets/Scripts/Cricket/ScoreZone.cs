using UnityEngine;
using UnityEngine.UI; 
using TMPro;          

public class ScoreZone : MonoBehaviour
{
    public int scoreValue = 10;               
    public ScoreManager scoreManager;   

           

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))   
        {
            scoreManager.AddScore(scoreValue);



            Destroy(other.gameObject);
        }
    }
}
