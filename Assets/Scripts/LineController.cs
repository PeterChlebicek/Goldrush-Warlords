using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField] private Transform[] unitTransforms;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Pokud lineRenderer neexistuje, vytvo�te ho.
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Z�sk�n� pouze jednotek, kter� jsou od sebe vzd�leny 10 a m�n�.
        List<Vector3> validPositions = new List<Vector3>();
        for (int i = 0; i < unitTransforms.Length; i++)
        {
            for (int j = i + 1; j < unitTransforms.Length; j++)
            {
                float distance = Vector3.Distance(unitTransforms[i].position, unitTransforms[j].position);
                if (distance <= 10f)
                {
                    validPositions.Add(unitTransforms[i].position);
                    validPositions.Add(unitTransforms[j].position);
                }
            }
        }

        // Nastaven� pozic lineRendereru.
        lineRenderer.positionCount = validPositions.Count;
        for (int i = 0; i < validPositions.Count; i++)
        {
            lineRenderer.SetPosition(i, validPositions[i]);
        }
    }
}
