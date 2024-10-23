using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    [HideInInspector] public int xValue;
    [HideInInspector] public int yValue;
    public List<GameObject> contains = new List<GameObject>();
}
