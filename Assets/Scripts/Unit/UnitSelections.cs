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
        if(instance != null && instance != this)
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
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
    }
    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if(!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            unitSelected.Remove(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void RemoveUnit()
    {
       foreach (var unit in unitSelected)
        {
            if (unit == null)
                unitSelected.Remove(unit);
        } 
    }

    public void DeselectAll()
    {
        foreach(var unit in unitSelected)
        {

            unit.transform.GetChild(0).gameObject.SetActive(false);
        }
        unitSelected.Clear();
    }
}
