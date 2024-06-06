using System.Collections;
using UnityEngine;

public class BanditCamp : MonoBehaviour
{
    public GameObject banditPrefab; // Prefab pro banditu
    public int maxBandits = 3; // Maxim�ln� po�et bandit� v t�bo�e
    private int currentBandits = 0; // Aktu�ln� po�et �iv�ch bandit�
    private float respawnTime = 3f; // �as, po kter�m se respawnuje bandita po smrti
    private Collider campCollider; // Kolize d�tsk�ho objektu s koliz� t�bora

    void Start()
    {
        // Z�sk�n� kolize d�tsk�ho objektu
        campCollider = GetComponentInChildren<Collider>();

        // Spawnuj po��te�n�ch 3 bandity
        for (int i = 0; i < maxBandits; i++)
        {
            SpawnBandit();
        }
    }

    void Update()
    {
        // Kontrola, zda nebyl smaz�n bandita
        if (currentBandits < maxBandits)
        {
            StartCoroutine(RespawnBandit(respawnTime));
        }
    }

    // Metoda pro spawnov�n� bandity
    void SpawnBandit()
    {
        // Pokud bylo dosa�eno maxim�ln�ho po�tu bandit�, dal�� se nevytvo��
        if (currentBandits >= maxBandits)
            return;

        // Vytvo�en� nov�ho bandity na n�hodn� pozici kolem t�bora
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject newBandit = Instantiate(banditPrefab, spawnPosition, Quaternion.identity);
        newBandit.GetComponent<Bandit>().SetCamp(this); // P�ed�n� odkazu na t�bor banditovi
        currentBandits++; // Zv��en� po�tu �iv�ch bandit�
    }

    // Metoda pro respawn bandity po smrti
    IEnumerator RespawnBandit(float respawnDelay)
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnBandit();
    }

    // Metoda pro z�sk�n� n�hodn� spawn pozice kolem t�bora
    Vector3 GetRandomSpawnPosition()
    {
        Vector3 spawnPosition = Vector3.zero;
        bool foundValidPosition = false;
        int attempts = 0;
        while (!foundValidPosition && attempts < 10)
        {
            spawnPosition = Random.insideUnitSphere * 5f + transform.position;
            spawnPosition.y = 0; // Nastaven� v��ky na 0 pro rovinu

            // Kontrola, zda je nov� pozice mimo kolizi d�tsk�ho objektu
            if (!campCollider.bounds.Contains(spawnPosition))
            {
                foundValidPosition = true;
            }

            attempts++;
        }

        // Pokud se nepoda�ilo naj�t platnou pozici, pou�ijeme n�hodnou pozici uvnit� jednotkov� koule
        if (!foundValidPosition)
        {
            Debug.LogWarning("Nepoda�ilo se naj�t platnou spawn pozici");
            spawnPosition = Random.insideUnitSphere * 5f + transform.position;
            spawnPosition.y = 0; // Nastaven� v��ky na 0 pro rovinu
        }

        return spawnPosition;
    }

    // Metoda pro sn�en� po�tu �iv�ch bandit� po smaz�n� objektu bandity
    public void BanditDied()
    {
        currentBandits--;
    }
}