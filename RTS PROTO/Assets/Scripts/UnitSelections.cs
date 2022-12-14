using System.Collections.Generic;
using UnityEngine;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    private static UnitSelections instance;
    public static UnitSelections Instance { get { return instance; } }

    public bool buildMode;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
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
        unitToAdd.GetComponent<Outline>().enabled = true;
        if (unitToAdd.GetComponent<UnitMovement>() != null) unitToAdd.GetComponent<UnitMovement>().enabled = true;
        if (unitToAdd.GetComponent<WorkerMovement>() != null) unitToAdd.GetComponent<WorkerMovement>().enabled = true;
    }
    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<Outline>().enabled = true;
            if (unitToAdd.GetComponent<UnitMovement>() != null) unitToAdd.GetComponent<UnitMovement>().enabled = true;
            if (unitToAdd.GetComponent<WorkerMovement>() != null) unitToAdd.GetComponent<WorkerMovement>().enabled = true;
        }
    }
    public void ControlClickDeselect(GameObject unitToDeselect)
    {
        if(unitSelected.Contains(unitToDeselect))
        {
            if (unitToDeselect.GetComponent<UnitMovement>() != null) unitToDeselect.GetComponent<UnitMovement>().enabled = false;
            if (unitToDeselect.GetComponent<WorkerMovement>() != null) unitToDeselect.GetComponent<WorkerMovement>().enabled = false;
            unitToDeselect.transform.GetChild(0).gameObject.SetActive(false);
            unitToDeselect.GetComponent<Outline>().enabled = false;

            unitSelected.Remove(unitToDeselect);
        }
    }
    public void DragSelect(GameObject unitToAdd)
    {
        if (!unitSelected.Contains(unitToAdd))
        {
            unitSelected.Add(unitToAdd);
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
            unitToAdd.GetComponent<Outline>().enabled = true;
            if (unitToAdd.GetComponent<UnitMovement>() != null) unitToAdd.GetComponent<UnitMovement>().enabled = true;
            if(unitToAdd.GetComponent<WorkerMovement>() != null) unitToAdd.GetComponent<WorkerMovement>().enabled = true;
        }
    }
    public void DragDeselect(GameObject unitToDeselect)
    {
        if (unitSelected.Contains(unitToDeselect))
        {
            unitSelected.Remove(unitToDeselect);
            unitToDeselect.transform.GetChild(0).gameObject.SetActive(false);
            unitToDeselect.GetComponent<Outline>().enabled = false;

            if (unitToDeselect.GetComponent<UnitMovement>() != null) unitToDeselect.GetComponent<UnitMovement>().enabled = false;
            if (unitToDeselect.GetComponent<WorkerMovement>() != null) unitToDeselect.GetComponent<WorkerMovement>().enabled = false;
        }
    }

    public void DeselectAll()
    {
        foreach (var unit in unitSelected)
        {
            unit.transform.GetChild(0).gameObject.SetActive(false);
            unit.GetComponent<Outline>().enabled = false;
            if (unit.GetComponent<UnitMovement>() != null) unit.GetComponent<UnitMovement>().enabled = false;
            if(unit.GetComponent<WorkerMovement>() != null) unit.GetComponent<WorkerMovement>().enabled = false;
        }
        unitSelected.Clear();
    }
}
