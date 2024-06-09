using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    protected int HP;

    [SerializeField]
    protected int priceGold;

    [SerializeField]
    protected int priceWood;

    public Type type; // P�id�n� t�to prom�nn�
    public enum Team
    {
        Player,
        Enemy,
        Aggresive
    }
    public enum Type
    {
        House,
        Barracks,
        TownCentre,
        Storage
    }
}
