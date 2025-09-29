using UnityEngine;

public class PortalManager : MonoBehaviour
{

    public string PlayerTag;
    public GameObject Destination;
    public GameObject player;

    private float portalCoolDown =3;


    private float cooldowntime;


    private bool onCoolDown;

    void Start()
    {
        cooldowntime = 0.0f;
        onCoolDown = false;
    }

    void Update()
    {
        if(onCoolDown)
        {
            cooldowntime -= Time.deltaTime;
            if(cooldowntime <= 0.0f)
            {
                onCoolDown = false;
            }
        }
    }


    public void StartCoolDown(){
        if(!onCoolDown){
            cooldowntime = portalCoolDown;
            onCoolDown = true;
        }
    }
/* 
private void OnTriggerEnter(Collider other){
    if(other.gameObject.tag == PlayerTag && !onCoolDown){
        StartCoolDown();

        player.transform.position = Destination.transform.position;
Debug.Log($"{gameObject.name} teleported {other.name} to {Destination.name} at {player.transform.position}");
    }
}
 */


private float timeInside = 0f;
public float requiredTimeInside = 2f; // seconds

private void OnTriggerStay(Collider other)
{
    if (other.CompareTag(PlayerTag) && !onCoolDown)
    {
        timeInside += Time.deltaTime;

        if (timeInside >= requiredTimeInside)
        {
            StartCoolDown();
            player.transform.position = Destination.transform.position;
            Debug.Log($"Teleported after {requiredTimeInside}s inside {gameObject.name}");
            timeInside = 0f;
        }
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.CompareTag(PlayerTag))
    {
        timeInside = 0f; // reset if player leaves early
    }
}

    
}
