using UnityEngine;

public class Resource : MonoBehaviour
{
    public int ResourcesRemaining = 200;
    public ResourceType resourceType; // Typ zdroje (døevo nebo zlato)

    public void CollectResource(int amount)
    {
        ResourcesRemaining -= amount;
        if (ResourcesRemaining <= 0)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
                Debug.Log(resourceType + " a jeho rodièovský objekt znièen.");
            }
            else
            {
                Destroy(gameObject);
                Debug.Log(resourceType + " znièen.");
            }
        }
    }
}
