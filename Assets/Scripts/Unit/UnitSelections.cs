using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    private static UnitSelections instance;
    public static UnitSelections Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitSelected.Add(unitToAdd);
        SetSphereActive(unitToAdd, true);
    }

    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            SetSphereActive(unitToAdd, true);
        }
        else
        {
            unitSelected.Remove(unitToAdd);
            SetSphereActive(unitToAdd, false);
        }
    }

    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            SetSphereActive(unitToAdd, true);
        }
    }

    public void RemoveUnit()
    {
        for (int i = unitSelected.Count - 1; i >= 0; i--)
        {
            if (unitSelected[i] == null)
                unitSelected.RemoveAt(i);
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            SetSphereActive(unit, false);
        }
        unitSelected.Clear();
    }

    private void SetSphereActive(GameObject unit, bool isActive)
    {
        Transform sphereTransform = unit.transform.Find("Sphere");

        if (sphereTransform != null)
        {
            sphereTransform.gameObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("Child 'Sphere' not found on unit: " + unit.name);
        }
    }
}
