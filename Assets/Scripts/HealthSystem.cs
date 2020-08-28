using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    private int currentHealth;

    public HealthSystem(int maxHealth)
    {
        currentHealth = maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void Damage(int amount)
    {
        currentHealth -= amount;
    }
}
