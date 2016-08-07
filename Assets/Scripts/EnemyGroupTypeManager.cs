using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGroupTypeManager : MonoBehaviour {
    public EnemyGroupType[] Types;
    private Dictionary<string, EnemyGroupType> groupLookup;

    void Awake() {
        // Create group lookup
        groupLookup = new Dictionary<string, EnemyGroupType>();
        foreach (EnemyGroupType type in Types) {
            groupLookup.Add(type.Name, type);
        }
    }
    public EnemyGroupType GetGroupByName(string name) {
        return groupLookup[name];
    }
}
