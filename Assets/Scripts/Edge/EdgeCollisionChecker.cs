using UnityEngine;
using UnityEngine.Events;

public class ObstacleCollisionChecker : MonoBehaviour
{
    public UnityEvent<GameObject> eventsToTrigger;
    void OnTriggerEnter(Collider other)
    {
        
        eventsToTrigger?.Invoke(other.gameObject);

    }
}
