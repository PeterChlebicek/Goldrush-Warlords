using UnityEngine;
using System;
using System.Collections;

public class Collector : MonoBehaviour
{
    public int MaxCapacity = 100;
    public float CollectionInterval = 1.5f;
    public string[] CollectibleTags = { "Tree", "Gold" };

    private int InventoryCapacity = 0;
    private bool isCollecting = false;
    private Transform targetResource;
    private ResourceManager resourceManager;
    private UnitMovement unitMovement;

    private void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
        unitMovement = GetComponent<UnitMovement>();
        InvokeRepeating("CollectResources", 0f, CollectionInterval);
    }

    private void Update()
    {
        if (InventoryCapacity >= MaxCapacity && targetResource != null)
        {
            ReturnToBase();
        }
    }

    private void CollectResources()
    {
        if (!isCollecting)
        {
            isCollecting = true;
            Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (Collider collider in colliders)
            {
                if (Array.Exists(CollectibleTags, tag => collider.CompareTag(tag)))
                {
                    targetResource = collider.transform;
                    StartCoroutine("CollectResource");
                    break;
                }
            }
        }
    }

    private IEnumerator CollectResource()
    {
        while (InventoryCapacity < MaxCapacity && targetResource != null)
        {
            yield return new WaitForSeconds(CollectionInterval);
            InventoryCapacity += 20;
            Debug.Log("Collected resources. Current capacity: " + InventoryCapacity);
        }
        isCollecting = false;
    }

    private void ReturnToBase()
    {
        // Find the closest HumanTC or HumanStorage
        GameObject[] bases = GameObject.FindGameObjectsWithTag("HumanTC");
        GameObject[] storages = GameObject.FindGameObjectsWithTag("HumanStorage");
        GameObject closestBase = FindClosestBase(bases, storages);

        // Move towards the closest base
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("triggered");
        if (other.CompareTag("HumanTC") || other.CompareTag("HumanStorage"))
        {
            SubmitResources(other);
        }
    }

    public void SubmitResources(Collider other)
    {
        Debug.Log("ASDASD", other.gameObject);
        if (resourceManager != null && InventoryCapacity > 0)
        {
            if (other.CompareTag("HumanTC"))
            {
                resourceManager.AddWood(InventoryCapacity);
            }
            else if (other.CompareTag("HumanStorage"))
            {
                resourceManager.AddGold(InventoryCapacity); // assuming gold is collected
            }
            InventoryCapacity = 0;
            Debug.Log("Resources submitted to base.");
        }
    }
}
