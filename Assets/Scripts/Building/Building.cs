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

    public enum Team
    {
        Player,
        Enemy,
        Aggresive
    }
}
