using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorOnClick : MonoBehaviour
{
    public List<Color> zabraneBarvy = new List<Color>(); // seznam zabran�ch barev

    public Image hracovaBarva;
    public Image botovaBarva;

    // Metoda pro zm�nu barvy
    public void ZmenBarvu(Image image)
    {
        Color novaBarva = NahodnaVolnaBarva();

        // Kontrola, zda je barva ji� zabran�
        while (zabraneBarvy.Contains(novaBarva))
        {
            novaBarva = NahodnaVolnaBarva();
        }

        // Zm�na barvy a p�id�n� do seznamu zabran�ch barev
        image.color = novaBarva;
        zabraneBarvy.Add(novaBarva);
    }

    // Metoda pro generov�n� n�hodn� voln� barvy
    private Color NahodnaVolnaBarva()
    {
        // Zde m��e� p�idat dal�� barvy, pokud bude� cht�t
        List<Color> volneBarvy = new List<Color>
        {
            Color.blue,
            Color.red,
            Color.green,
            new Color(0.6f, 0, 0.8f), // fialov�
            Color.yellow
        };

        // Vyber n�hodnou barvu
        int index = Random.Range(0, volneBarvy.Count);
        Color novaBarva = volneBarvy[index];

        return novaBarva;
    }
}