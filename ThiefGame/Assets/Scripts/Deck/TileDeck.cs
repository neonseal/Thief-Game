using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileDeck : MonoBehaviour
{
    [SerializeField] GameObject[] tileTypes;
    [SerializeField] int deckSize = 5;
    [SerializeField] int handSize = 3;

    private List<GameObject> _tileDeck;
    private List<GameObject> _hand;
    private float  _selectedTile;

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

    public GameObject DrawTileFromHand(int selectedCard)
    {
        if(selectedCard > 2 || selectedCard < 0)
        {
            selectedCard = 0;
            Debug.LogError("selected card outside range of hand");
        }
      
        Debug.Log("card " + selectedCard + " selected");
        var drawnCard = _hand[selectedCard];
        _hand.RemoveAt(selectedCard);
        _hand.Add(DrawTile());

        return drawnCard;
     
    }
    
    private void DiscardHand()
    {
        _hand.Clear();
    }
    

}
