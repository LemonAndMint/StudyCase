using UnityEngine;
using UnityEngine.Events;

public class EdgeCollisionChecker : MonoBehaviour
{
    public UnityEvent<GameObject> eventsToTrigger;

    void OnTriggerEnter(Collider other)
    {
        
        // No need for cheking the "other" collider for validation. "Edge" only able to collide with "Ground". 
        eventsToTrigger?.Invoke(other.gameObject);

    }
}
