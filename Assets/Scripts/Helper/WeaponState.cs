using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Stats", menuName = "Weapon/Stats")]
public class WeaponState : ScriptableObject
{
    [Range(0, 100)]
    public int Damage = 1;

    [Range(0f, 20)]
    public int Range = 2;

    [Range(0f, 20f)]
    public float Frequency = 5f;

    [Range(0f, 10f)]
    public int Amount = 1;

    [Range(0f, 100f)]
    public int Speed = 10;
}
