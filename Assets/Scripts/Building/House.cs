using UnityEngine;

public class House : Building
{
    private ResourceManager resourceManager;

    private void Start()
    {
        // Najdeme ResourceManager na scénì
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            // Pøidáme +10 k populaci
            resourceManager.AddPopulation(10);
        }
        else
        {
            Debug.LogError("ResourceManager not found!");
        }
    }

    private void OnDestroy()
    {
        if (resourceManager != null)
        {
            // Odebereme -10 z populace pøi znièení domu
            resourceManager.RemovePopulation(10);
        }
    }
}