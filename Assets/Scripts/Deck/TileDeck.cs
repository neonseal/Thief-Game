using System.Collections.Generic;
using UnityEngine;

public class TileDeck : MonoBehaviour {
    [SerializeField] GameObject[] tileTypes;
    [SerializeField] int deckSize = 5;
    [SerializeField] int handSize = 3;
    [SerializeField] TileDeckController tileDeckController;


    private List<GameObject> _tileDeck;
    private List<GameObject> _hand;
    private int _selectedTile;

    public static event System.Action TileDrawn;


    private void Awake() {
        _tileDeck = new();
        _hand = new();

        //populate deck with random tiles from the tile types
        for (int i = 0; i < deckSize; i++) {
            _tileDeck.Add(tileTypes[Random.Range(0, 2)]);
        }

        for (int i = 0; i < handSize; i++) {
            _hand.Add(DrawTile());
        }

        TileDrawn?.Invoke();
    }



    private GameObject DrawTile() {
        if (!TileDeckIsEmpty()) {
            GameObject drawnTile = _tileDeck[0];
            _tileDeck.Remove(drawnTile);
            TileDrawn?.Invoke();
            return drawnTile;

        } else {
            Debug.Log("Deck is Empty");
            return null;
        }

    }


    public bool TileDeckIsEmpty() {
        return _tileDeck.Count == 0;
    }


    //draw a tile from the current hand and then replace it

    public GameObject DrawTileFromHand(int selectedTile) {
        //checks if selectedTile is in range of hand
        if (selectedTile > _hand.Count - 1 || selectedTile < 0) {
            selectedTile = 0;
            Debug.LogError("selected tile outside range of hand");
        }

        var drawnTile = _hand[selectedTile];
        _hand.RemoveAt(selectedTile);

        if (!TileDeckIsEmpty()) {
            _hand.Insert(selectedTile, DrawTile());

        }

        TileDrawn.Invoke();
        return drawnTile;

    }

    //returns tiles in hand. doesnt return the full gameobject
    public List<BaseTile> GetHandTiles() {
        List<BaseTile> handTiles = new();

        if (HandIsEmpty()) {
            return handTiles;
        }



        for (int i = 0; i < _hand.Count; i++) {

            handTiles.Add(_hand[i].GetComponent<BaseTile>());
        }

        return handTiles;
    }

    public void DiscardHand() {
        _hand.Clear();

        for (int i = 0; i < handSize; i++) {
            if (TileDeckIsEmpty()) {
                return;
            }

            _hand.Add(DrawTile());
        }
    }

    public void SetSelectedTile(int value) {
        _selectedTile = value;
    }

    public bool HandIsEmpty() {
        return _hand.Count == 0;
    }
}
