using FischlWorks_FogWar;
using System;
using UnityEngine;
using static FischlWorks_FogWar.csFogWar;

public enum UnitType
{
    Infantry,
    Archer,
    Strong,
    Collector
}

public class Unit : MonoBehaviour
{
    public enum Team
    {
        Player,
        Enemy,
        Aggresive
    }

    public UnitType Type;
    public Team UnitTeam; // Nová vlastnost pro tým jednotky
    public int MaxHealth;
    public int Health;
    public int SightRange;
    public int MinDmg;
    public int MaxDmg;
    public int Defense;
    public int WoodCost;
    public int FoodCost;
    public int GoldCost;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        UnitSelections.Instance.unitList.Add(this.gameObject);

        FogRevealer fr = new FogRevealer(transform, SightRange, true);
    }
    void Update()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void OnDestroy()
    {
        UnitSelections.Instance.unitList.Remove(this.gameObject);
    }
    // Metoda pro útok
    public void Attack(Unit targetUnit)
    {
        if (this.UnitTeam == Team.Player && targetUnit.UnitTeam == Team.Enemy || targetUnit.UnitTeam == Team.Aggresive)
        {
            // Náhodné poškození v rozmezí od MinDmg do MaxDmg
            int damage = UnityEngine.Random.Range(MinDmg, MaxDmg + 1);
            damage -= targetUnit.Defense;

            // Ochrana pøed negativním poškozením
            damage = Mathf.Max(damage, 0);

            // Odeètení poškození od životù cílové jednotky
            targetUnit.Health -= damage;

            // Pokud cílová jednotka zemøe, mùžete pøidat další logiku nebo akce
            if (targetUnit.Health <= 0)
            {
                Debug.Log("Enemy unit destroyed!");
                UnitSelections.Instance.RemoveUnit();
                Destroy(targetUnit.gameObject);
            }
            else
            {
                Debug.Log("Enemy unit damaged! Remaining health: " + targetUnit.Health);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            //if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
            //{
                if(collision.gameObject.GetComponent<Unit>().UnitTeam == Team.Enemy) {


                    var enemy = GetComponent<Unit>();
                    var enemyHealthBar = GetComponentInChildren<Healthbar>();

                    int actualDamage = UnityEngine.Random.Range(MinDmg, MaxDmg) - Defense;

                    enemy.Health -= actualDamage;

                    enemyHealthBar.UpdateHealthBar(enemy.MaxHealth, enemy.Health);

                    Debug.Log("Attack Enemy");
                }
            //}

            

        }
    }
}
