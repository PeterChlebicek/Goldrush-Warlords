using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMovement : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab; // Prefab objektu "Marker"
    private GameObject markerInstance; // Instance vytvo�en�ho markeru

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // Prav� tla��tko my�i
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                CreateMarker(hit.point);
            }
        }
    }

    private void CreateMarker(Vector3 position)
    {
        // Nastaven� sou�adnice Y na 0
        position.y = 0;

        // Zni�en� p�edchoz� instance markeru, pokud existuje
        if (markerInstance != null)
        {
            Destroy(markerInstance);
        }

        // Vytvo�en� nov� instance markeru na dan�m m�st�
        markerInstance = Instantiate(markerPrefab, position, Quaternion.identity);

        // Zni�en� markeru po 3 sekund�ch
        Destroy(markerInstance, 3f);
    }
}
