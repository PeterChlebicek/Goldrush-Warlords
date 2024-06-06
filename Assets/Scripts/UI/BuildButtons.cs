using FischlWorks_FogWar;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static FischlWorks_FogWar.csFogWar;

public class BuildButtons : MonoBehaviour
{
    public GameObject House;
    public GameObject Barracks;
    public GameObject TownCentre;
    public GameObject Storage;

    public csFogWar fogRevealerManager; // Pøiøaïte instanci FogRevealerManager do editoru Unity
    
    public void buildHouse()
    {
        Instantiate(House);
    }
    public void buildBarracks()
    {
        Instantiate(Barracks);
    }
    public void buildTownCentre()
    {
        Instantiate(TownCentre);
    }
    public void buildStorage()
    {
        Instantiate(Storage);
    }
}
