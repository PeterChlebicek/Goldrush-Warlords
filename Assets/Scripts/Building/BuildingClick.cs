using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingClick : MonoBehaviour
{
    private Camera myCam;
    private GameObject selectedBuilding;

    public LayerMask clickable;
    public LayerMask ground;

    public GameObject buildingPanel;
    public GameObject collectorButton;
    public GameObject collectorPrefab;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                SelectBuilding(hit.collider.gameObject); // Select building
            }
            else
            {
                DeselectBuilding(); // Deselect building
            }
        }
    }

    void SelectBuilding(GameObject building)
    {
        if (selectedBuilding != null)
        {
            DeselectBuilding(); // Deselect previously selected building
        }

        selectedBuilding = building;
        ShowSphere(selectedBuilding, true);
        Debug.Log("Building selected: " + building.name);
        ShowCollectorButton(true);

    }

    void DeselectBuilding()
    {
        if (selectedBuilding != null)
        {
            ShowSphere(selectedBuilding, false);
            Debug.Log("Building deselected: " + selectedBuilding.name);
            selectedBuilding = null;
            ShowCollectorButton(false);
        }
    }

    void ShowSphere(GameObject building, bool show)
    {
        Transform sphereTransform = building.transform.Find("Sphere");
        if (sphereTransform != null)
        {
            sphereTransform.gameObject.SetActive(show);
        }
        else
        {
            Debug.LogWarning("Sphere child not found in building: " + building.name);
        }
    }
    void ShowCollectorButton(bool show)
    {
        if (show)
        {
            if (selectedBuilding != null && collectorButton == null)
            {
                // Create collector button if it doesn't exist
                collectorButton = Instantiate(collectorPrefab, buildingPanel.transform);
                Button buttonComponent = collectorButton.GetComponent<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.onClick.AddListener(CreateCollector);
                }
                else
                {
                    Debug.LogWarning("Button component not found on collector prefab.");
                }
            }
        }
        else
        {
            // Hide collector button if it exists
            if (collectorButton != null)
            {
                Destroy(collectorButton);
                collectorButton = null;
            }
        }
    }

    void CreateCollector()
    {
        // Start coroutine to wait 5 seconds before spawning collector
        StartCoroutine(SpawnCollectorAfterDelay());
    }

    IEnumerator SpawnCollectorAfterDelay()
    {
        yield return new WaitForSeconds(5f);

        if (selectedBuilding != null)
        {
            // Spawn collector 2 units away from selected building
            Vector3 spawnPosition = selectedBuilding.transform.position + selectedBuilding.transform.forward * 2f;
            Instantiate(collectorPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
