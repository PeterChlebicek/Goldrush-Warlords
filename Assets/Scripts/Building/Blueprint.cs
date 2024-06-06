using FischlWorks_FogWar;
using System.Collections;
using UnityEngine;

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
        if (!isBuilding) // "Pl�novac�" verze budovy sleduje my�, pokud nestav�me
        {
            UpdateBlueprintPosition();

            if (Input.GetMouseButton(0))
            {
                buildPos = hit.point; // Pozice pro stavbu je stejn� jako pozice, kde hr�� klikne
                StartCoroutine(Build());
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                isBuilding = true; // "Pl�novac�" verze budovy p�estane sledovat my�
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
        // �as konstrukce
        yield return new WaitForSeconds(2f);
        // Fin�ln� verze budovy se d� m�sto "pl�novac�" verze budovy.
        Instantiate(prefab, buildPos, transform.rotation);
        Destroy(gameObject);
    }
}