using UnityEngine;

public class House : Building
{
    private ResourceManager resourceManager;

    private void Start()
    {
        // Najdeme ResourceManager na sc�n�
        resourceManager = FindObjectOfType<ResourceManager>();
        if (resourceManager != null)
        {
            // P�id�me +10 k populaci
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
            // Odebereme -10 z populace p�i zni�en� domu
            resourceManager.RemovePopulation(10);
        }
    }
}