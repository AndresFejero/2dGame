using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public float movementSpeed;
    public float attRange;
    public int attDamage;
    public int healthPoints;
    public int maxHealthPoints;

    public void DamageTaken(int damageInput)
    {
        healthPoints -= damageInput;
    }
}
