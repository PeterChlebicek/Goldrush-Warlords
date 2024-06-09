using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerMovement : MonoBehaviour
{
    [SerializeField] private GameObject markerPrefab; // Prefab objektu "Marker"
    private GameObject markerInstance; // Instance vytvoøeného markeru

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))  // Pravé tlaèítko myši
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
        // Nastavení souøadnice Y na 0
        position.y = 0;

        // Znièení pøedchozí instance markeru, pokud existuje
        if (markerInstance != null)
        {
            Destroy(markerInstance);
        }

        // Vytvoøení nové instance markeru na daném místì
        markerInstance = Instantiate(markerPrefab, position, Quaternion.identity);

        // Znièení markeru po 3 sekundách
        Destroy(markerInstance, 3f);
    }
}
