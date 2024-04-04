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
        //sends selected tile from the ui to model
        tileDeck.SetSelectedTile(value);
        deckUI.RestartGenerateRoutine();
        _selectedTileValue = value;
    }

    private void OnDiscardButtonPressed()
    {
      

        if(!tileDeck.TileDeckIsEmpty())
        {
            tileDeck.DiscardHand();
        }
        else
        {
            Debug.Log("No more cards to discard");
        }
        deckUI.UpdateCurrentHandTiles(tileDeck.GetHandTiles());
    }

    private void OnTileDrawn()
    {
        deckUI.UpdateCurrentHandTiles(tileDeck.GetHandTiles());
    }

    void OnEnable()
    {
        TileDeck.TileDrawn += OnTileDrawn;
        DeckUI.TileFromHandSelected += OnTileFromHandSelected;
        DeckUI.DiscardButtonPressed += OnDiscardButtonPressed;

        deckUI.UpdateCurrentHandTiles(tileDeck.GetHandTiles());


    }

    public int GetSelectedTileValue()
    {
        return _selectedTileValue;
    }

    void OnDisable()
    {
        
    }

}
