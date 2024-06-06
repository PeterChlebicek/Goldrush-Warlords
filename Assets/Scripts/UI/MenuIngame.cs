using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIngame : MonoBehaviour
{
    public GameObject inGameMenu; // Reference na in-game menu
    public GameObject helpMenu;

    private bool isPaused = false; // Indik�tor, zda je hra pozastavena

    private Button[] allButtons; // Pole pro v�echna tla��tka v hierarchii
    private Dictionary<Button, bool> buttonStates; // Ulo�en� p�vodn�ch stav� interaktibility tla��tek

    void Start()
    {
        // P�ipojte metodu ToggleMenu k ud�losti onClick tla��tka UI
        if (inGameMenu != null)
        {
            Button menuButton = inGameMenu.GetComponentInChildren<Button>();
            if (menuButton != null)
            {
                menuButton.onClick.AddListener(ToggleMenu);
            }
        }

        // Najd�te v�echna tla��tka v hierarchii
        allButtons = FindObjectsOfType<Button>();
        buttonStates = new Dictionary<Button, bool>();

        // Ulo�te p�vodn� stavy interaktibility tla��tek
        foreach (Button button in allButtons)
        {
            buttonStates[button] = button.interactable;
        }
    }

    void Update()
    {
        // Kontrola stisku kl�vesy pro otev�en�/zav�en� menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
            ShowHelp();
        }
    }

    // Metoda pro otev�en�/zav�en� in-game menu a zastaven�/odstartov�n� �asu
    public void ToggleMenu()
    {
        isPaused = !isPaused;

        // Zastaven�/odstartov�n� �asu
        Time.timeScale = isPaused ? 0 : 1;

        // Zobrazen�/skryt� in-game menu
        inGameMenu.SetActive(isPaused);

        // Nastaven� interaktibility tla��tek
        foreach (Button button in allButtons)
        {
            // Pokud je tla��tko v panelu in-game menu, nech�me ho interaktivn�
            if (inGameMenu.transform.IsChildOf(button.transform) || button.transform.IsChildOf(inGameMenu.transform))
            {
                button.interactable = true;
            }
            else
            {
                // Pokud je hra pozastavena, deaktivujeme tla��tka
                button.interactable = !isPaused && buttonStates[button];
            }
        }
    }
    public void ShowHelp()
    {
        isPaused = !isPaused;

        // Zastaven�/odstartov�n� �asu
        Time.timeScale = isPaused ? 0 : 1;

        // Zobrazen�/skryt� in-game menu
        helpMenu.SetActive(isPaused);

        // Nastaven� interaktibility tla��tek
        foreach (Button button in allButtons)
        {
            // Pokud je tla��tko v panelu in-game menu, nech�me ho interaktivn�
            if (helpMenu.transform.IsChildOf(button.transform) || button.transform.IsChildOf(helpMenu.transform))
            {
                button.interactable = true;
            }
            else
            {
                // Pokud je hra pozastavena, deaktivujeme tla��tka
                button.interactable = !isPaused && buttonStates[button];
            }
        }
    }
}
    