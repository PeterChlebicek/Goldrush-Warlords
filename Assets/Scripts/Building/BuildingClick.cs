using UnityEngine;
using UnityEngine.UI;

public class BuildingClick : MonoBehaviour
{
    private Camera myCam;
    private GameObject selectedBuilding;

    public LayerMask buildingsLayer;
    public GameObject buildingPanel;
    public GameObject buttonPrefab; // Prefab tlaèítka
    public GameObject unitPrefab; // Prefab jednotky

    private GameObject collectorButtonInstance;
    private GameObject[] barracksButtons = new GameObject[3];

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

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, buildingsLayer))
            {
                SelectBuilding(hit.collider.gameObject);
            }
            else
            {
                DeselectBuilding();
            }
        }
    }

    void SelectBuilding(GameObject building)
    {
        if (selectedBuilding != null)
        {
            DeselectBuilding();
        }

        selectedBuilding = building;
        ShowSphere(selectedBuilding, true);
        Debug.Log("Building selected: " + building.name);

        Building buildingComponent = building.GetComponent<Building>();
        if (buildingComponent != null)
        {
            if (buildingComponent.type == Building.Type.TownCentre)
            {
                CreateCollectorButton();
            }
            else if (buildingComponent.type == Building.Type.Barracks)
            {
                CreateBarracksButtons();
            }
        }
    }

    void DeselectBuilding()
    {
        if (selectedBuilding != null)
        {
            ShowSphere(selectedBuilding, false);
            Debug.Log("Building deselected: " + selectedBuilding.name);
            selectedBuilding = null;

            if (collectorButtonInstance != null)
            {
                Destroy(collectorButtonInstance);
            }

            foreach (var button in barracksButtons)
            {
                if (button != null)
                {
                    Destroy(button);
                }
            }
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

    void CreateCollectorButton()
    {
        if (collectorButtonInstance == null)
        {
            collectorButtonInstance = Instantiate(buttonPrefab, buildingPanel.transform);
            Text buttonText = collectorButtonInstance.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Collector";
            }
            Button button = collectorButtonInstance.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(SpawnCollector);
            }
        }
    }

    void CreateBarracksButtons()
    {
        string[] buttonNames = { "Footman", "Mage", "Paladin" };

        for (int i = 0; i < buttonNames.Length; i++)
        {
            if (barracksButtons[i] == null)
            {
                barracksButtons[i] = Instantiate(buttonPrefab, buildingPanel.transform);
                Text buttonText = barracksButtons[i].GetComponentInChildren<Text>();
                if (buttonText != null)
                {
                    buttonText.text = buttonNames[i];
                }
                Button button = barracksButtons[i].GetComponent<Button>();
                if (button != null)
                {
                    int index = i; // Capture the current value of i
                    button.onClick.AddListener(() => SpawnUnit(index)); // Use a lambda to capture the index value
                }
            }
        }
    }

    public void SpawnCollector()
    {
        if (selectedBuilding != null)
        {
            Vector3 spawnPosition = selectedBuilding.transform.position + selectedBuilding.transform.forward * 3f;
            Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
        }
    }

    void SpawnUnit(int index)
    {
        if (selectedBuilding != null)
        {
            Vector3 spawnPosition = selectedBuilding.transform.position + selectedBuilding.transform.forward * 3f;
            // Instantiate unitPrefab based on the index
            // For example, you may want to have different prefabs for each unit type
            // Here, assuming unitPrefab is a placeholder, you might want to replace it with actual unit prefabs
            Instantiate(unitPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
