using UnityEngine;

public class CatCowboy : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    
    public int GetDamage()
    {
        return damage;
    }
}
