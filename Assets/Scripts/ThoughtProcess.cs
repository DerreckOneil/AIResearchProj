using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu]
[Serializable]
public class ThoughtProcess : ScriptableObject
{
    [SerializeField] List<CardinalDirection> directions = new List<CardinalDirection>();
    public List<CardinalDirection> Directions
    {
        get { return directions; }
    }
}
