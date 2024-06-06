using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int gold = 225;
    [SerializeField] private int wood = 300;
    [SerializeField] private int population = 10;

    [SerializeField] private TextMeshProUGUI woodText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI populationText;

    public event Action<int> OnGoldChanged;
    public event Action<int> OnWoodChanged;
    public event Action<int> OnPopulationChanged;

    private void Start()
    {
        UpdateUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Added " + amount + " gold. Total gold: " + gold);
        OnGoldChanged?.Invoke(gold);
        UpdateUI();
    }

    public bool RemoveGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log("Removed " + amount + " gold. Total gold: " + gold);
            OnGoldChanged?.Invoke(gold);
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough gold!");
            return false;
        }
    }

    public int GetGold()
    {
        return gold;
    }

    public void AddWood(int amount)
    {
        wood += amount;
        Debug.Log("Added " + amount + " wood. Total wood: " + wood);
        OnWoodChanged?.Invoke(wood);
        UpdateUI();
    }

    public bool RemoveWood(int amount)
    {
        if (wood >= amount)
        {
            wood -= amount;
            Debug.Log("Removed " + amount + " wood. Total wood: " + wood);
            OnWoodChanged?.Invoke(wood);
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough wood!");
            return false;
        }
    }

    public int GetWood()
    {
        return wood;
    }

    public void AddPopulation(int amount)
    {
        population += amount;
        Debug.Log("Added " + amount + " population. Total population: " + population);
        OnPopulationChanged?.Invoke(population);
        UpdateUI();
    }

    public bool RemovePopulation(int amount)
    {
        if (population >= amount)
        {
            population -= amount;
            Debug.Log("Removed " + amount + " population. Total population: " + population);
            OnPopulationChanged?.Invoke(population);
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough population!");
            return false;
        }
    }

    public int GetPopulation()
    {
        return population;
    }

    private void UpdateUI()
    {
        if (woodText != null)
        {
            woodText.text = "" + wood;
        }

        if (goldText != null)
        {
            goldText.text = "" + gold;
        }

        if (populationText != null)
        {
            populationText.text = "" + population;
        }
    }
}
