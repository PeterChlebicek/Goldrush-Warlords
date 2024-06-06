using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private List<GameObject> players = new List<GameObject>();

    public string playerTag = "Player";
    public float searchRadius = 5f;
    public float moveSpeed = 3f;

    private GameObject Player;

    void Update()
    {
        SearchForBlueCapsule();
        MoveTowardsBlueCapsule();
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
                Vector3 direction = closestPlayer.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                transform.Translate(direction * moveSpeed * Time.deltaTime);
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
}
