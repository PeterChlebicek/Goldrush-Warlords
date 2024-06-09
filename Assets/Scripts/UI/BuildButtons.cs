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

    public csFogWar fogRevealerManager; // Pøiøaïte instanci FogRevealerManager do editoru Unity
    public ResourceManager resourceManager; // Pøiøaïte instanci ResourceManager do editoru Unity

    // Ceny budov
    public int houseGoldCost = 100;
    public int houseWoodCost = 50;

    public int barracksGoldCost = 200;
    public int barracksWoodCost = 150;

    public int townCentreGoldCost = 500;
    public int townCentreWoodCost = 300;

    public int storageGoldCost = 150;
    public int storageWoodCost = 100;

    public TMP_Text notificationTextPrefab; // Reference na TextMeshProUGUI prefab pro zobrazení notifikace

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

    // Metoda pro zobrazení notifikace
    private void ShowNotification()
    {
        TMP_Text notificationText = Instantiate(notificationTextPrefab, transform.parent); // Vytvoøení TextMeshPro na urèeném rodièi
        notificationText.rectTransform.localPosition = new Vector3(20, 353, 0); // Nastavení pozice
        notificationText.rectTransform.sizeDelta = new Vector2(200, 50); // Nastavení rozmìrù
        notificationText.text = "You don't have enough resources!"; // Nastavení textu
        StartCoroutine(HideNotification(notificationText)); // Spuštìní metody pro skrytí notifikace
    }

    // Metoda pro skrytí notifikace s animací
    private IEnumerator HideNotification(TMP_Text notificationText)
    {
        float duration = 2f; // Délka animace (sekundy)
        float elapsedTime = 0f; // Uplynulý èas

        // Postupnì zmenšuj velikost textu a posouve ho dolù
        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration); // Lineárnì interpoluj prùhlednost
            notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, alpha); // Nastav prùhlednost
            notificationText.rectTransform.localPosition -= new Vector3(0f, 1f, 0f); // Posuò text dolù
            elapsedTime += Time.deltaTime; // Zvyš uplynulý èas o èas posledního snímku
            yield return null; // Poèkej na další snímek
        }

        Destroy(notificationText.gameObject); // Zniè TextMeshPro po dokonèení animace
    }
}
