using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/Object")]
public class InteractableObjectData : ScriptableObject
{ 
    public GameObject prefab;
    public Item.ItemType requiredToolType;
}
