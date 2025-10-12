using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager Instance;

    [HideInInspector]
    public string activeZone = "";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SetActiveZone(string zoneName)
    {
        Debug.Log("Active Zone set to: " + zoneName);
        activeZone = zoneName;
    }
}
