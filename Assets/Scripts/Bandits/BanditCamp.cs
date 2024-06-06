using System.Collections;
using UnityEngine;

public class BanditCamp : MonoBehaviour
{
    public GameObject banditPrefab; // Prefab pro banditu
    public int maxBandits = 3; // Maximální poèet banditù v táboøe
    private int currentBandits = 0; // Aktuální poèet živých banditù
    private float respawnTime = 3f; // Èas, po kterém se respawnuje bandita po smrti
    private Collider campCollider; // Kolize dìtského objektu s kolizí tábora

    void Start()
    {
        // Získání kolize dìtského objektu
        campCollider = GetComponentInChildren<Collider>();

        // Spawnuj poèáteèních 3 bandity
        for (int i = 0; i < maxBandits; i++)
        {
            SpawnBandit();
        }
    }

    void Update()
    {
        // Kontrola, zda nebyl smazán bandita
        if (currentBandits < maxBandits)
        {
            StartCoroutine(RespawnBandit(respawnTime));
        }
    }

    // Metoda pro spawnování bandity
    void SpawnBandit()
    {
        // Pokud bylo dosaženo maximálního poètu banditù, další se nevytvoøí
        if (currentBandits >= maxBandits)
            return;

        // Vytvoøení nového bandity na náhodné pozici kolem tábora
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject newBandit = Instantiate(banditPrefab, spawnPosition, Quaternion.identity);
        newBandit.GetComponent<Bandit>().SetCamp(this); // Pøedání odkazu na tábor banditovi
        currentBandits++; // Zvýšení poètu živých banditù
    }

    // Metoda pro respawn bandity po smrti
    IEnumerator RespawnBandit(float respawnDelay)
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnBandit();
    }

    // Metoda pro získání náhodné spawn pozice kolem tábora
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool foundValidPosition = false;
        int attempts = 0;
        while (!foundValidPosition && attempts < 10)
        {
            spawnPosition = Random.insideUnitSphere * 5f + transform.position;
            spawnPosition.y = 0; // Nastavení výšky na 0 pro rovinu

            // Kontrola, zda je nová pozice mimo kolizi dìtského objektu
            if (!campCollider.bounds.Contains(spawnPosition))
            {
                foundValidPosition = true;
            }

            attempts++;
        }

        // Pokud se nepodaøilo najít platnou pozici, použijeme náhodnou pozici uvnitø jednotkové koule
        if (!foundValidPosition)
        {
            Debug.LogWarning("Nepodaøilo se najít platnou spawn pozici");
            spawnPosition = Random.insideUnitSphere * 5f + transform.position;
            spawnPosition.y = 0; // Nastavení výšky na 0 pro rovinu
        }

        return spawnPosition;
    }

    // Metoda pro snížení poètu živých banditù po smazání objektu bandity
    public void BanditDied()
    {
        currentBandits--;
    }
}