using FischlWorks_FogWar;
using System.Collections;
using UnityEngine;
using static FischlWorks_FogWar.csFogWar;

public class Blueprint : MonoBehaviour
{
    RaycastHit hit;
    Vector3 movePoint;
    public GameObject prefab;
    public LayerMask layerMask = new LayerMask();
    private bool isBuilding = false;

    Vector3 buildPos;
    void Start()
    {
        UpdateBlueprintPosition();
    }

    void Update()
    {
        if (!isBuilding) // "Plánovací" verze budovy sleduje myš, pokud nestavíme
        {
            UpdateBlueprintPosition();

            if (Input.GetMouseButton(0))
            {
                buildPos = hit.point; // Pozice pro stavbu je stejná jako pozice, kde hráè klikne
                StartCoroutine(Build());
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                isBuilding = true; // "Plánovací" verze budovy pøestane sledovat myš

                // Pøidání budovy do systému mlhy (FogWar)
                csFogWar fogWar = FindObjectOfType<csFogWar>(); // Hledá objekt s csFogWar skriptem v aktuální scénì
                if (fogWar != null)
                {
                    FogRevealer revealer = new FogRevealer(transform, 12, true);
                    fogWar.AddFogRevealer(revealer);
                }
                else
                {
                    Debug.LogWarning("Objekt s csFogWar skriptem nenalezen v scénì. Budova nebude pøidána do systému mlhy.");
                }
            }
        }
    }

    void UpdateBlueprintPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 50000.0f, layerMask))
        {
            movePoint = hit.point;
            transform.position = movePoint;
        }
    }

    IEnumerator Build()
    {
        // Èas konstrukce
        yield return new WaitForSeconds(5f);
        // Finální verze budovy se dá místo "plánovací" verze budovy.
        Instantiate(prefab, buildPos, transform.rotation);
        Destroy(gameObject);
    }
}
