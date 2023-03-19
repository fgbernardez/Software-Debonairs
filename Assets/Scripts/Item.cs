using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New object", menuName = "Item")]
public class Item : ScriptableObject 
{
   public TileBase tile;
   public Sprite image;
   public Type type;
   public bool stackable = true;
}

public enum Type 
{
   Consumable,
   Equippable
}
