using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuIngame : MonoBehaviour
{
    public GameObject inGameMenu; // Reference na in-game menu
    public GameObject helpMenu;

    private bool isPaused = false; // Indikátor, zda je hra pozastavena

    private Button[] allButtons; // Pole pro všechna tlaèítka v hierarchii
    private Dictionary<Button, bool> buttonStates; // Uložení pùvodních stavù interaktibility tlaèítek

    void Start()
    {
        // Pøipojte metodu ToggleMenu k události onClick tlaèítka UI
        if (inGameMenu != null)
        {
            Button menuButton = inGameMenu.GetComponentInChildren<Button>();
            if (menuButton != null)
            {
                menuButton.onClick.AddListener(ToggleMenu);
            }
        }

        // Najdìte všechna tlaèítka v hierarchii
        allButtons = FindObjectsOfType<Button>();
        buttonStates = new Dictionary<Button, bool>();

        // Uložte pùvodní stavy interaktibility tlaèítek
        foreach (Button button in allButtons)
        {
            buttonStates[button] = button.interactable;
        }
    }

    void Update()
    {
        // Kontrola stisku klávesy pro otevøení/zavøení menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
            ShowHelp();
        }
    }

    // Metoda pro otevøení/zavøení in-game menu a zastavení/odstartování èasu
    public void ToggleMenu()
    {
        isPaused = !isPaused;

        // Zastavení/odstartování èasu
        Time.timeScale = isPaused ? 0 : 1;

        // Zobrazení/skrytí in-game menu
        inGameMenu.SetActive(isPaused);

        // Nastavení interaktibility tlaèítek
        foreach (Button button in allButtons)
        {
            // Pokud je tlaèítko v panelu in-game menu, necháme ho interaktivní
            if (inGameMenu.transform.IsChildOf(button.transform) || button.transform.IsChildOf(inGameMenu.transform))
            {
                button.interactable = true;
            }
            else
            {
                // Pokud je hra pozastavena, deaktivujeme tlaèítka
                button.interactable = !isPaused && buttonStates[button];
            }
        }
    }
    public void ShowHelp()
    {
        isPaused = !isPaused;

        // Zastavení/odstartování èasu
        Time.timeScale = isPaused ? 0 : 1;

        // Zobrazení/skrytí in-game menu
        helpMenu.SetActive(isPaused);

        // Nastavení interaktibility tlaèítek
        foreach (Button button in allButtons)
        {
            // Pokud je tlaèítko v panelu in-game menu, necháme ho interaktivní
            if (helpMenu.transform.IsChildOf(button.transform) || button.transform.IsChildOf(helpMenu.transform))
            {
                button.interactable = true;
            }
            else
            {
                // Pokud je hra pozastavena, deaktivujeme tlaèítka
                button.interactable = !isPaused && buttonStates[button];
            }
        }
    }
}
    