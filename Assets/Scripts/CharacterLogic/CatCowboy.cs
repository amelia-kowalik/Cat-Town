using UnityEngine;

public class CatCowboy : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //gameObject.SetActive(false);
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetDamage()
    {
        return damage;
    }
}
