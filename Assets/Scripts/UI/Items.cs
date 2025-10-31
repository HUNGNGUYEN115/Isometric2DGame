using UnityEngine;
[CreateAssetMenu(fileName = "New item", menuName = "Inventory/Item")]
public class Items : ScriptableObject  
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string itemname;
    public Itemtype itemtype;
    public Sprite   icon;
    public virtual void Use()
    {
        Debug.Log("Using item");
    }

}
public enum Itemtype { Potion, Weapon}
