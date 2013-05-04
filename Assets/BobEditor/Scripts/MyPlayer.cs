using UnityEngine;
using System.Collections;

public class MyPlayer : MonoBehaviour {

    [SerializeField]
    int armor = 75;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    [SerializeField]
    int damage = 25;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    [SerializeField]
    GameObject gun;
    public UnityEngine.GameObject Gun
    {
        get { return gun; }
        set { gun = value; }
    }
}
