using UnityEngine;
using UnityEngine.Events;

public class ObstacleCollisionChecker : MonoBehaviour
{
    public UnityEvent<GameObject> eventsToTrigger;
    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("wörks");
        eventsToTrigger?.Invoke(other.gameObject);

    }
}
