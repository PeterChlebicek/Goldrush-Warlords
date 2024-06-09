using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();

    public string playerTag = "Player";
    public float searchRadius = 5f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f; // Rychlost otáèení

    private GameObject Player;
    private Animator animator;
    private bool isMoving = true;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            SearchForBlueCapsule();
            MoveTowardsBlueCapsule();
        }
    }

    void SearchForBlueCapsule()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        players.Clear(); // Vyèisti seznam hráèských jednotek

        foreach (var collider in colliders)
        {
            if (collider.CompareTag(playerTag))
            {
                players.Add(collider.gameObject);
            }
        }
    }

    void MoveTowardsBlueCapsule()
    {
        if (players.Count > 0)
        {
            GameObject closestPlayer = GetClosestPlayer();
            if (closestPlayer != null)
            {
                animator.SetBool("IsMoving", true);
                Vector3 direction = closestPlayer.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                // Pohyb nepøítele
                transform.Translate(direction * moveSpeed * Time.deltaTime);

                // Otoèení k hráèi
                RotateTowards(closestPlayer.transform.position);
            }
        }
    }

    GameObject GetClosestPlayer()
    {
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestPlayer = player;
                closestDistance = distance;
            }
        }

        return closestPlayer;
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            isMoving = false;
            animator.SetBool("IsMoving", false);
        }
    }
}
