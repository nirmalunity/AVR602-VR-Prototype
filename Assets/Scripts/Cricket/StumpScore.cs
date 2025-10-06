using UnityEngine;

public class StumpScoreZone : MonoBehaviour
{
    public int scoreValue = -1;          
    public ScoreManager scoreManager;   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            
            scoreManager.AddScore(scoreValue);

            Debug.Log("Stumps hit by ball!");

          
            Destroy(collision.gameObject);
        }
    }
}
