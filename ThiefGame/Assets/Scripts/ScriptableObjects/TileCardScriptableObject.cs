using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TileCardScriptableObject", order = 1)]
public class TileCardScriptableObject : ScriptableObject
{
    public bool IsNorthOpen;
    public bool IsSouthOpen;
    public bool IsEastOpen;
    public bool IsWestOpen;
    public TileType TileType;
}
