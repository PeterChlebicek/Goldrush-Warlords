using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    [SerializeField] private float speed = 5f;
    private bool isMoving = false;
    private Vector3 targetPosition;

    // Formace parameters
    [SerializeField] private float formationSpacing = 2f; // Vzdálenost mezi jednotlivými jednotkami ve formaci
    [SerializeField] private int unitsPerRow = 4; // Poèet jednotek v jedné øadì

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (UnitSelections.Instance.unitSelected.Contains(gameObject))
        {
            if (Input.GetMouseButtonDown(1))  // Pravé tlaèítko myši
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    SetFormation(hit.point, ray.direction);
                }
            }

            if (isMoving)
            {
                MoveToClickPoint(targetPosition);
            }
        }
    }



    private void SetFormation(Vector3 destination, Vector3 clickDirection)
    {
        // Nastavení cílových pozic pro jednotky ve formaci
        List<Vector3> formationPositions = CalculateFormationPositions(destination, UnitSelections.Instance.unitSelected.Count, clickDirection);

        // Nastavení cílové pozice každé jednotky
        for (int i = 0; i < UnitSelections.Instance.unitSelected.Count; i++)
        {
            GameObject selectedUnit = UnitSelections.Instance.unitSelected[i];
            UnitMovement unitMovement = selectedUnit.GetComponent<UnitMovement>();
            unitMovement.targetPosition = formationPositions[i];
            unitMovement.isMoving = true;
        }
    }

    private List<Vector3> CalculateFormationPositions(Vector3 centerPosition, int unitCount, Vector3 clickDirection)
    {
        List<Vector3> formationPositions = new List<Vector3>();

        // Výpoèet pozic ve formaci kolem støedu
        int rowCount = Mathf.CeilToInt((float)unitCount / unitsPerRow); // Poèet øad ve formaci
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < unitsPerRow; j++)
            {
                int index = i * unitsPerRow + j;
                if (index >= unitCount)
                {
                    break;
                }

                Vector3 offset = Vector3.zero;
                // Dynamické urèení smìru formace podle kliknutí
                if (Mathf.Abs(clickDirection.x) > Mathf.Abs(clickDirection.z))
                {
                    offset = new Vector3(j * formationSpacing, 0f, i * formationSpacing);
                }
                else
                {
                    offset = new Vector3(i * formationSpacing, 0f, j * formationSpacing);
                }
                Vector3 formationPosition = centerPosition + offset;
                formationPositions.Add(formationPosition);
            }
        }
        return formationPositions;
    }

    public void MoveToClickPoint(Vector3 destination)
    {
        navMeshAgent.acceleration = speed;
        navMeshAgent.SetDestination(destination);

        // Pokud jsou jednotky blízko své cílové pozice, zastaví se
        if (Vector3.Distance(transform.position, destination) < 0.5f)
        {
            isMoving = false;
        }
    }
}