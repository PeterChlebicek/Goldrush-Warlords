using UnityEngine;
using System.Collections;
using System;

public class Collector : MonoBehaviour
{
    public int MaxCapacity = 100;
    public int InventoryCapacity = 0;
    public float CollectionRate = 20f; // Množství suroviny sebere za urèitý interval
    public string[] CollectibleTags = { "Tree", "Gold" };

    private bool isCollecting = false;
    private Transform targetResource;
    private ResourceManager resourceManager;
    private UnitMovement unitMovement;
    private ResourceTextCanvas resourceTextCanvas;

    // Pøidána vlastnost pro sledování typu sbírané suroviny
    public ResourceType CurrentResourceType = ResourceType.None;

    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        unitMovement = GetComponent<UnitMovement>();
        resourceTextCanvas = GetComponentInChildren<ResourceTextCanvas>();
        UpdateResourceText();
    }

    private void Update()
    {
        if (InventoryCapacity >= MaxCapacity && targetResource != null)
        {
            ReturnToBase();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Array.Exists(CollectibleTags, tag => other.CompareTag(tag)))
        {
            if (!isCollecting)
            {
                targetResource = other.transform;
                Resource resource = targetResource.GetComponent<Resource>();
                if (resource != null)
                {
                    CurrentResourceType = resource.resourceType;
                    StartCoroutine("CollectResource", resource);
                }
            }
        }

        if (other.CompareTag("HumanTC") || other.CompareTag("HumanStorage"))
        {
            SubmitResources(other);
        }
    }

    private IEnumerator CollectResource(Resource resource)
    {
        isCollecting = true;
        while (InventoryCapacity < MaxCapacity && resource.ResourcesRemaining > 0)
        {
            yield return new WaitForSeconds(1f); // Sbírá každou sekundu
            int amountToCollect = (int)CollectionRate;
            resource.CollectResource(amountToCollect);
            InventoryCapacity += amountToCollect;
            UpdateResourceText();
            Debug.Log("Sbírá suroviny. Aktuální kapacita: " + InventoryCapacity);

            // Kontrola, zda je zdroj znièen
            if (resource.ResourcesRemaining <= 0)
            {
                targetResource = null;
                isCollecting = false;
                // Hledání dalšího stejného zdroje
                FindNextResource();
            }
        }
        isCollecting = false;
    }

    private void FindNextResource()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 50f); // Hledá v okruhu 50 jednotek
        foreach (Collider collider in colliders)
        {
            if (Array.Exists(CollectibleTags, tag => collider.CompareTag(tag)))
            {
                Resource resource = collider.GetComponent<Resource>();
                if (resource != null && resource.resourceType == CurrentResourceType)
                {
                    targetResource = collider.transform;
                    StartCoroutine("CollectResource", resource);
                    break;
                }
            }
        }
        // Pokud nenajde další zdroj, vrátí se na základnu
        if (targetResource == null)
        {
            ReturnToBase();
        }
    }

    private void ReturnToBase()
    {
        // Najde nejbližší HumanTC nebo HumanStorage
        GameObject[] bases = GameObject.FindGameObjectsWithTag("HumanTC");
        GameObject[] storages = GameObject.FindGameObjectsWithTag("HumanStorage");
        GameObject closestBase = FindClosestBase(bases, storages);

        // Pøesune se k nejbližší základnì
        if (closestBase != null)
        {
            if (unitMovement != null)
            {
                unitMovement.MoveToClickPoint(closestBase.transform.position);
            }
        }
    }

    private GameObject FindClosestBase(GameObject[] bases, GameObject[] storages)
    {
        GameObject closestBase = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject baseObject in bases)
        {
            float distance = Vector3.Distance(transform.position, baseObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBase = baseObject;
            }
        }
        foreach (GameObject storage in storages)
        {
            float distance = Vector3.Distance(transform.position, storage.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestBase = storage;
            }
        }
        return closestBase;
    }

    public void SubmitResources(Collider other)
    {
        if (resourceManager != null && InventoryCapacity > 0)
        {
            if (CurrentResourceType == ResourceType.Wood)
            {
                resourceManager.AddWood(InventoryCapacity);
                Debug.Log("Døevo odevzdáno do skladu.");
            }
            else if (CurrentResourceType == ResourceType.Gold)
            {
                resourceManager.AddGold(InventoryCapacity);
                Debug.Log("Zlato odevzdáno do skladu.");
            }
            InventoryCapacity = 0;
            UpdateResourceText();
            CurrentResourceType = ResourceType.None; // Reset po odevzdání
        }
    }

    private void UpdateResourceText()
    {
        if (resourceTextCanvas != null)
        {
            resourceTextCanvas.UpdateInventoryText(InventoryCapacity);
        }
    }
}
