using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPasteScrpt : MonoBehaviour
{
    public int HealAmount; // Ammount item heals for 
    public HealthBarScript HealthBarScript;
    public Stats Stats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealth()
    {
        Stats.Health += HealAmount; // Add health

        RemoveItemFromInVentory();
    }

    public void RemoveItemFromInVentory()
    {

    }
}
