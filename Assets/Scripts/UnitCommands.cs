using UnityEngine;
using UnityEngine.AI;

public class UnitCommands : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Unit unit;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unit = GetComponent<Unit>();
    }

    void Update()
    {
        // Pøíkaz k pohybu (pravé tlaèítko myši)
        if (Input.GetMouseButtonDown(1))
        {
            MoveToCommand();
        }

        // Pøíkaz k útoku (levé tlaèítko myši)
        if (Input.GetMouseButtonDown(0))
        {
            AttackCommand();
        }

        // Pøíkaz k zastavení ('S' klávesa)
        if (Input.GetKeyDown(KeyCode.S))
        {
            StopCommand();
        }
    }

    void MoveToCommand()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }

    void AttackCommand()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Unit targetUnit = hit.collider.GetComponent<Unit>();

            if (targetUnit != null && targetUnit.UnitTeam != unit.UnitTeam)
            {
                navMeshAgent.SetDestination(targetUnit.transform.position);

                if (Vector3.Distance(transform.position, targetUnit.transform.position) < navMeshAgent.stoppingDistance)
                {
                    AttackTarget(targetUnit);
                }
            }
        }
    }

    void AttackTarget(Unit targetUnit)
    {
        int damage = Random.Range(unit.MinDmg, unit.MaxDmg + 1) - targetUnit.Defense;
        damage = Mathf.Max(0, damage);

        Debug.Log("Jednotka " + unit.name + " zasadila " + damage + " poškození jednotce " + targetUnit.name);
    }

    void StopCommand()
    {
        navMeshAgent.ResetPath();
    }
}
