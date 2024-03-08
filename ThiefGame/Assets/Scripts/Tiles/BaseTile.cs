using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public bool isNorthOpen;
    public bool isSouthOpen;
    public bool isEastOpen;
    public bool isWestOpen;
    public TileCoordinate coordinate;
}
