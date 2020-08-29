using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyMemberController : MonoBehaviour
{
    public string partyMemberName;
    private HealthSystem healthSystem;
    public Weapon weapon;

    private void Awake()
    {
        healthSystem = new HealthSystem(100);
        weapon = Instantiate(weapon, transform);
        HideWeapon();
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }

    public void Attack()
    {
        weapon.Attack(); 
    }

    public void HideWeapon()
    {
        weapon.gameObject.SetActive(false);
    }

    public void ShowWeapon()
    {
        weapon.gameObject.SetActive(true);
    }
}
