using UnityEngine;
using System.Collections;
using System;

public class Collector : MonoBehaviour
{
    public int MaxCapacity = 100;
    public int InventoryCapacity = 0;
    public float CollectionRate = 20f; // Mno�stv� suroviny sebere za ur�it� interval
    public string[] CollectibleTags = { "Tree", "Gold" };

    private bool isCollecting = false;
    private Transform targetResource;
    private ResourceManager resourceManager;
    private UnitMovement unitMovement;
    private ResourceTextCanvas resourceTextCanvas;

    // P�id�na vlastnost pro sledov�n� typu sb�ran� suroviny
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
            yield return new WaitForSeconds(1f); // Sb�r� ka�dou sekundu
            int amountToCollect = (int)CollectionRate;
            resource.CollectResource(amountToCollect);
            InventoryCapacity += amountToCollect;
            UpdateResourceText();
            Debug.Log("Sb�r� suroviny. Aktu�ln� kapacita: " + InventoryCapacity);

            // Kontrola, zda je zdroj zni�en
            if (resource.ResourcesRemaining <= 0)
            {
                targetResource = null;
                isCollecting = false;
                // Hled�n� dal��ho stejn�ho zdroje
                FindNextResource();
            }
        }
        isCollecting = false;
    }

    private void FindNextResource()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 50f); // Hled� v okruhu 50 jednotek
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
        // Pokud nenajde dal�� zdroj, vr�t� se na z�kladnu
        if (targetResource == null)
        {
            ReturnToBase();
        }
    }

    private void ReturnToBase()
    {
        // Najde nejbli��� HumanTC nebo HumanStorage
        GameObject[] bases = GameObject.FindGameObjectsWithTag("HumanTC");
        GameObject[] storages = GameObject.FindGameObjectsWithTag("HumanStorage");
        GameObject closestBase = FindClosestBase(bases, storages);

        // P�esune se k nejbli��� z�kladn�
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
                Debug.Log("D�evo odevzd�no do skladu.");
            }
            else if (CurrentResourceType == ResourceType.Gold)
            {
                resourceManager.AddGold(InventoryCapacity);
                Debug.Log("Zlato odevzd�no do skladu.");
            }
            InventoryCapacity = 0;
            UpdateResourceText();
            CurrentResourceType = ResourceType.None; // Reset po odevzd�n�
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
