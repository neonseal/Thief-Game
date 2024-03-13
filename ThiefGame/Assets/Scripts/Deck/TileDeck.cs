using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileDeck : MonoBehaviour
{
    [SerializeField] GameObject[] tileTypes;
    [SerializeField] int deckSize = 5;
    [SerializeField] int handSize = 3;
    [SerializeField]  TileDeckController tileDeckController;


    private List<GameObject> _tileDeck;
    private List<GameObject> _hand;
    private int  _selectedTile;

    public static event System.Action TileDrawn;


    private void Awake()
    {
        _tileDeck = new();
        _hand = new();

        //populate deck with random tiles from the tile types
        for(int i = 0; i < deckSize; i++)
        {
            _tileDeck.Add(tileTypes[Random.Range(0, 2)]);
        }

        for (int i = 0; i < handSize; i++)
        {
            _hand.Add(DrawTile());
        }
    }

    private void OnEnable()
    {
    }

    private GameObject DrawTile()
    {
        GameObject drawnTile = _tileDeck[0];
        _tileDeck.Remove(drawnTile);
        return drawnTile;
    }


    public bool IsEmpty()
    {
        return _tileDeck.Count == 0;
    }

    public GameObject DrawTileFromHand(int selectedTile)
    {
        if(selectedTile > 2 || selectedTile < 0)
        {
            selectedTile = 0;
            Debug.LogError("selected tile outside range of hand");
        }
      
        Debug.Log("tile  " + selectedTile + " selected");
        var drawnTile = _hand[selectedTile];
        _hand.RemoveAt(selectedTile);
        _hand.Insert(selectedTile,DrawTile());
        TileDrawn.Invoke();

        return drawnTile;
     
    }

    public List<BaseTile> GetHandTiles()
    {
        List<BaseTile> handTiles = new();

        for(int i = 0; i < handSize; i++)
        {
            handTiles.Add(_hand[i].GetComponent<BaseTile>());
        }

        return handTiles;
    }
    
    private void DiscardHand()
    {
        _hand.Clear();
    }

    public void SetSelectedTile(int value)
    {
        _selectedTile = value;
    }


    

}
