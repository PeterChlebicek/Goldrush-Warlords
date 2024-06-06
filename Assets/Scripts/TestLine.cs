using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLine : MonoBehaviour
{
    public Transform Blue;
    public Transform Red;
    public float detectionRadius = 5f;
    public float moveSpeed = 2f;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        // Zjistit vzdálenost mezi BlueCapsule a RedCapsule
        float distance = Vector3.Distance(Blue.position, Red.position);

        // Pokud jsou v dosahu, vytvoøit Line Renderer a pøibližovat k sobì
        if (distance < detectionRadius)
        {
            // Vytvoøit nebo aktualizovat Line Renderer
            if (lineRenderer.positionCount == 0)
            {
                lineRenderer.positionCount = 2;
            }

            lineRenderer.SetPosition(0, Blue.position);
            lineRenderer.SetPosition(1, Red.position);

            // Pøibližovat k sobì
            Blue.position = Vector3.MoveTowards(Blue.position, Red.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Pokud nejsou v dosahu, vypnout Line Renderer
            lineRenderer.positionCount = 0;
        }
    }
}

