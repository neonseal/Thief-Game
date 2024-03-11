using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject _initialTile;
    [SerializeField]
    private GameObject newTile;
    [SerializeField]
    private GameObject TileGhostPrefab;

    [HideInInspector]
    public static Dictionary<TileCoordinate, Tile> existingTiles = new();

    [HideInInspector]
    public static Dictionary<TileCoordinate, TileGhost> availableTiles = new();

    private TileDeck deck;

    private int selectedTileValue;

    // Start is called before the first frame update
    void Start()
    {
        Tile[] initialTiles = transform.GetComponentsInChildren<Tile>(false);
        foreach (Tile t in initialTiles) {
            existingTiles.Add(t.coordinate, t);
        }
        ExpandFromTile(_initialTile.GetComponent<Tile>());

        deck = GetComponent<TileDeck>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddTile(TileGhost tileGhostToReplace) {

        //will get rid of the ghosting and what not later
        //just a basic fix to stop errors 
        if(deck.IsEmpty())
        {
            Debug.Log("deck empty");
            return;
        }
        newTile = deck.DrawTileFromHand(selectedTileValue);
        newTile = Instantiate(this.newTile, new Vector3(tileGhostToReplace.coordinate.x, 0f, tileGhostToReplace.coordinate.y), Quaternion.identity);

        newTile.name = "Tile (" + tileGhostToReplace.coordinate.x + "," + tileGhostToReplace.coordinate.y + ")";
        newTile.transform.SetParent(transform);
        Tile newTileComponent = newTile.GetComponent<Tile>();
        newTileComponent.coordinate = tileGhostToReplace.coordinate;
        ExpandFromTile(newTileComponent);
        existingTiles.Add(tileGhostToReplace.coordinate, newTileComponent);
        availableTiles.Remove(tileGhostToReplace.coordinate);
        Destroy(tileGhostToReplace.gameObject);
    }

    private void ExpandFromTile(Tile tile) {
        TileCoordinate newTileCoordinate;
        if (tile.isNorthOpen && tile.coordinate.y < sbyte.MaxValue) {
            newTileCoordinate = new TileCoordinate(tile.coordinate.x, (sbyte) (tile.coordinate.y + 1));
            if (availableTiles.ContainsKey(newTileCoordinate)) {
                availableTiles[newTileCoordinate].isSouthOpen = true;
            }
            else if (!existingTiles.ContainsKey(newTileCoordinate)) {
                TileGhost newTileGhost = InstantiateTileGhost(newTileCoordinate);
                availableTiles.Add(newTileCoordinate, newTileGhost);
            }
        }
        if (tile.isSouthOpen && tile.coordinate.y > sbyte.MinValue) {
            newTileCoordinate = new TileCoordinate(tile.coordinate.x, (sbyte) (tile.coordinate.y - 1));
            if (availableTiles.ContainsKey(newTileCoordinate)) {
                availableTiles[newTileCoordinate].isNorthOpen = true;
            }
            else if (!existingTiles.ContainsKey(newTileCoordinate)) {
                TileGhost newTileGhost = InstantiateTileGhost(newTileCoordinate);
                availableTiles.Add(newTileCoordinate, newTileGhost);
            }
        }
        if (tile.isEastOpen && tile.coordinate.x < sbyte.MaxValue) {
            newTileCoordinate = new TileCoordinate((sbyte) (tile.coordinate.x + 1), tile.coordinate.y);
            if (availableTiles.ContainsKey(newTileCoordinate)) {
                availableTiles[newTileCoordinate].isWestOpen = true;
            }
            else if (!existingTiles.ContainsKey(newTileCoordinate)) {
                TileGhost newTileGhost = InstantiateTileGhost(newTileCoordinate);
                availableTiles.Add(newTileCoordinate, newTileGhost);
            }
        }
        if (tile.isWestOpen && tile.coordinate.x > sbyte.MinValue) {
            newTileCoordinate = new TileCoordinate((sbyte) (tile.coordinate.x - 1), tile.coordinate.y);
           if (availableTiles.ContainsKey(newTileCoordinate)) {
                availableTiles[newTileCoordinate].isEastOpen = true;
            }
            else if (!existingTiles.ContainsKey(newTileCoordinate)) {
                TileGhost newTileGhost = InstantiateTileGhost(newTileCoordinate);
                availableTiles.Add(newTileCoordinate, newTileGhost);
            }
        }
    }

    private TileGhost InstantiateTileGhost(TileCoordinate coordinate) {
        GameObject newTileGhostGO = Instantiate(TileGhostPrefab, new Vector3(coordinate.x, 0f, coordinate.y), Quaternion.identity);
        newTileGhostGO.transform.parent = this.transform;
        TileGhost newTileGhost = newTileGhostGO.GetComponent<TileGhost>();
        newTileGhost.coordinate = coordinate;
        newTileGhostGO.name = "TileGhost (" + coordinate.x + "," + coordinate.y + ")";
        return newTileGhost;
    }

    private void OnEnable()
    {
        DeckUI.CardFromHandSelected += OnTileFromHandSelected;
    }

    private void OnDisable()
    {
        DeckUI.CardFromHandSelected -= OnTileFromHandSelected;
    }

    void OnTileFromHandSelected(int value)
    {
        selectedTileValue = value;
    }
}
