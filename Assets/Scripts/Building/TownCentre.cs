using UnityEngine;

public class TownCentre : Building
{
    private ResourceManager resourceManager;

    private void Start()
    {
        type = Type.TownCentre;
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            // Pøidá +20 k populaci 
            resourceManager.AddPopulation(20);
        }
        else
        {
            Debug.LogError("ResourceManager not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("KOLIZE" + other.gameObject.name + " " + other.gameObject.tag);
        var collector = other.GetComponent<Collector>();
        if(collector != null)
        {
            collector.SubmitResources(GetComponent<Collider>());
        }
    }

    private void OnDestroy()
    {
        if (resourceManager != null)
        {
            // Odebere -20 z populace pøi znièení TC
            resourceManager.RemovePopulation(20);
        }
    }
}