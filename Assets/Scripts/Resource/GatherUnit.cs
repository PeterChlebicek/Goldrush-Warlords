using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherUnit : MonoBehaviour, IUnit
{
    private bool iddle = true;

    [SerializeField]
    private float speed = 5f;

    public bool IsIdle() => iddle;

    public void MoveTo(Vector3 position, float stopDistance, Action onMoved)
    {
        float distance = Vector2.Distance(transform.position, position);
        if (distance < stopDistance)
        {
            Debug.Log("Moved to destination");
            onMoved?.Invoke();
            iddle = true;
        }
        else
        {
            Debug.Log("Moving");
            transform.position = Vector3.MoveTowards(transform.position, position, speed * Time.deltaTime);
        }
    }

    public void PlayAnimation(Vector3 lookPosition, Action onAnimationFinished)
    {
        iddle = false;
        Debug.Log("Playing animation");
        iddle = true;
        onAnimationFinished?.Invoke();
    }
}
