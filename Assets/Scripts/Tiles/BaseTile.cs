using UnityEngine;

public abstract class BaseTile : MonoBehaviour {
    public bool isNorthOpen;
    public bool isSouthOpen;
    public bool isEastOpen;
    public bool isWestOpen;
    public TileCoordinate coordinate;

    public string Name;

    public BaseTile northTile;
    public BaseTile southTile;
    public BaseTile eastTile;
    public BaseTile westTile;
}
