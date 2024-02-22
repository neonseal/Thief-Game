using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDeckManager : MonoBehaviour
{
    private Tile[] _tileTypes;
    private List<Tile> _tileDeck;

    private List<Tile> _hand;
    private Tile _selectedTile;

    private void Awake()
    {
        
    }

    private void DrawTile()
    {

    }

    private void SelectTile(Tile tile)
    {
        _selectedTile = tile;
    }

    private void DiscardHand()
    {
        _hand.Clear();
    }


}
