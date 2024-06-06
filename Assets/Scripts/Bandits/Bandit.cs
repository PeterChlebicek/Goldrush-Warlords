using UnityEngine;

public class Bandit : MonoBehaviour
{
    private BanditCamp camp; // Reference na tábor, ke kterému bandita patøí

    // Metoda pro nastavení tábora banditovi
    public void SetCamp(BanditCamp camp)
    {
        this.camp = camp;
    }

    // Metoda pro oznaèení smrti bandity
    public void Die()
    {
        if (camp != null)
        {
            camp.BanditDied(); // Snížení poètu živých banditù v táboøe
        }
        Destroy(gameObject); // Znièení objektu bandity
    }
}
