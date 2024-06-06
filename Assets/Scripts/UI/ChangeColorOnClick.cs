using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorOnClick : MonoBehaviour
{
    public List<Color> zabraneBarvy = new List<Color>(); // seznam zabraných barev

    public Image hracovaBarva;
    public Image botovaBarva;

    // Metoda pro zmìnu barvy
    public void ZmenBarvu(Image image)
    {
        Color novaBarva = NahodnaVolnaBarva();

        // Kontrola, zda je barva již zabraná
        while (zabraneBarvy.Contains(novaBarva))
        {
            novaBarva = NahodnaVolnaBarva();
        }

        // Zmìna barvy a pøidání do seznamu zabraných barev
        image.color = novaBarva;
        zabraneBarvy.Add(novaBarva);
    }

    // Metoda pro generování náhodné volné barvy
    private Color NahodnaVolnaBarva()
    {
        // Zde mùžeš pøidat další barvy, pokud budeš chtít
        List<Color> volneBarvy = new List<Color>
        {
            Color.blue,
            Color.red,
            Color.green,
            new Color(0.6f, 0, 0.8f), // fialová
            Color.yellow
        };

        // Vyber náhodnou barvu
        int index = Random.Range(0, volneBarvy.Count);
        Color novaBarva = volneBarvy[index];

        return novaBarva;
    }
}