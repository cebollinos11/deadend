using UnityEngine;
using System.Collections;

[System.Serializable]
public class EnemyGroupType {
    public string Name;
    public bool IsRandom = false;
    public int AmountIfRandom = 1;
    public AbstractEnemy[] Enemies;
    public float InnerDelay = 0.15f;
}
