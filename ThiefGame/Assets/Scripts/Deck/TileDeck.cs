using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileDeck : MonoBehaviour
{
    [SerializeField] GameObject[] tileTypes;
    private List<GameObject> _tileDeck;
    [SerializeField] int deckSize = 5;

    private List<Tile> _hand;
    private Tile _selectedTile;

    private void Awake()
    {
        _tileDeck = new();

        //populate deck with random tiles from the tile types
        for(int i = 0; i < deckSize; i++)
        {
            _tileDeck.Add(tileTypes[Random.Range(0, 2)]);
        }
    }

    public GameObject DrawTile()
    {
        GameObject drawnTile = _tileDeck[0];
        _tileDeck.Remove(drawnTile);
        return drawnTile;
    }


    public bool IsEmpty()
    {
        return _tileDeck.Count == 0;
    }

    /*
    private void SelectTile(Tile tile)
    {
        _selectedTile = tile;
    }

    private void DiscardHand()
    {
        _hand.Clear();
    }
    */

}
