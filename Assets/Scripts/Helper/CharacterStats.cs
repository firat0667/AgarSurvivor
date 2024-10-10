using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{

    [Range(0, 100)]
    public float FirstScale;

    [Range(0f, 200f)]
    public float MaxHP = 100f;

    [Range(0f, 20f)]
    public float MoveSpeed = 5f;

    [Range(0f, 50f)]
    public int AttackPower = 10;
}

