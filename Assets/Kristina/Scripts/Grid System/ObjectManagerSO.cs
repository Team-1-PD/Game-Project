using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Object Manager")]
public class ObjectManagerSO : ScriptableObject
{
    //TODO:: replace string with a PlaceableObject script for data handling
    //<ID, object>
    public Dictionary<string, GameObject> placeableObjects = new Dictionary<string,GameObject>();

    public List<GameObject> objects = new List<GameObject>();
}
