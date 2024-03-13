using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDeckController : MonoBehaviour
{
    [SerializeField] private TileDeck tileDeck;
    [SerializeField] private DeckUI deckUI;

    private int _selectedTileValue;

    private void OnTileFromHandSelected(int value)
    {
        //send data to model
        tileDeck.SetSelectedTile(value);
        deckUI.RestartGenerateRoutine();
        _selectedTileValue = value;
    }

    private void OnTileDrawn()
    {
        deckUI.UpdateCurrentHandTiles(tileDeck.GetHandTiles());
    }

    void OnEnable()
    {
        TileDeck.TileDrawn += OnTileDrawn;
        DeckUI.TileFromHandSelected += OnTileFromHandSelected;
    }

    public int GetSelectedTileValue()
    {
        return _selectedTileValue;
    }

    void OnDisable()
    {
        
    }

}
