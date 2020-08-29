using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public HealthSystem healthSystem;

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
    }
}
