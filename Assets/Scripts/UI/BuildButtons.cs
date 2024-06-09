using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildButtons : MonoBehaviour
{
    public GameObject House;
    public GameObject Barracks;
    public GameObject TownCentre;
    public GameObject Storage;

    public csFogWar fogRevealerManager; // P�i�a�te instanci FogRevealerManager do editoru Unity
    public ResourceManager resourceManager; // P�i�a�te instanci ResourceManager do editoru Unity

    // Ceny budov
    public int houseGoldCost = 100;
    public int houseWoodCost = 50;

    public int barracksGoldCost = 200;
    public int barracksWoodCost = 150;

    public int townCentreGoldCost = 500;
    public int townCentreWoodCost = 300;

    public int storageGoldCost = 150;
    public int storageWoodCost = 100;

    public TMP_Text notificationTextPrefab; // Reference na TextMeshProUGUI prefab pro zobrazen� notifikace

    private void Start()
    {
        notificationTextPrefab.gameObject.SetActive(true);
    }

    public void buildHouse()
    {
        if (resourceManager.RemoveGold(houseGoldCost) && resourceManager.RemoveWood(houseWoodCost))
        {
            Instantiate(House);
        }
        else
        {
            ShowNotification();
        }
    }

    public void buildBarracks()
    {
        if (resourceManager.RemoveGold(barracksGoldCost) && resourceManager.RemoveWood(barracksWoodCost))
        {
            Instantiate(Barracks);
        }
        else
        {
            ShowNotification();
        }
    }

    public void buildTownCentre()
    {
        if (resourceManager.RemoveGold(townCentreGoldCost) && resourceManager.RemoveWood(townCentreWoodCost))
        {
            Instantiate(TownCentre);
        }
        else
        {
            ShowNotification();
        }
    }

    public void buildStorage()
    {
        if (resourceManager.RemoveGold(storageGoldCost) && resourceManager.RemoveWood(storageWoodCost))
        {
            Instantiate(Storage);
        }
        else
        {
            ShowNotification();
        }
    }

    // Metoda pro zobrazen� notifikace
    private void ShowNotification()
    {
        TMP_Text notificationText = Instantiate(notificationTextPrefab, transform.parent); // Vytvo�en� TextMeshPro na ur�en�m rodi�i
        notificationText.rectTransform.localPosition = new Vector3(20, 353, 0); // Nastaven� pozice
        notificationText.rectTransform.sizeDelta = new Vector2(200, 50); // Nastaven� rozm�r�
        notificationText.text = "You don't have enough resources!"; // Nastaven� textu
        StartCoroutine(HideNotification(notificationText)); // Spu�t�n� metody pro skryt� notifikace
    }

    // Metoda pro skryt� notifikace s animac�
    private IEnumerator HideNotification(TMP_Text notificationText)
    {
        float duration = 2f; // D�lka animace (sekundy)
        float elapsedTime = 0f; // Uplynul� �as

        // Postupn� zmen�uj velikost textu a posouve ho dol�
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration); // Line�rn� interpoluj pr�hlednost
            notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, alpha); // Nastav pr�hlednost
            notificationText.rectTransform.localPosition -= new Vector3(0f, 1f, 0f); // Posu� text dol�
            elapsedTime += Time.deltaTime; // Zvy� uplynul� �as o �as posledn�ho sn�mku
            yield return null; // Po�kej na dal�� sn�mek
        }

        Destroy(notificationText.gameObject); // Zni� TextMeshPro po dokon�en� animace
    }
}
