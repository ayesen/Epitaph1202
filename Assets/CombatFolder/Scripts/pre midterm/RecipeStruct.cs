using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Recipe
{
    public string namae;
    public List<InventoryDict> materials;
    public GameObject Outcome;
}
