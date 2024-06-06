using UnityEngine;

public class Bandit : MonoBehaviour
{
    private BanditCamp camp; // Reference na t�bor, ke kter�mu bandita pat��

    // Metoda pro nastaven� t�bora banditovi
    public void SetCamp(BanditCamp camp)
    {
        this.camp = camp;
    }

    // Metoda pro ozna�en� smrti bandity
    public void Die()
    {
        if (camp != null)
        {
            camp.BanditDied(); // Sn�en� po�tu �iv�ch bandit� v t�bo�e
        }
        Destroy(gameObject); // Zni�en� objektu bandity
    }
}
