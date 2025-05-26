using UnityEngine;

//Cat Eastwood
public class Expert : MonoBehaviour
{
    [SerializeField] private int damage = 15;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    

    public int GetDamage()
    {
        return damage;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
