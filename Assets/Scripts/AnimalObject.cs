using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product", menuName = "AnimalProduct")]
public class AnimalObject : ScriptableObject
{
    public string animalName;
    public Sprite[] animalStages;
    public float timeBTWStages;
}
