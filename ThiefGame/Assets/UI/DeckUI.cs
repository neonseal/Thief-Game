using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private UIDocument document;
    [SerializeField] private StyleSheet styleSheet;

    [SerializeField] private TileDeckController tileDeckController;

    public static event Action<int> TileFromHandSelected;

    private List<BaseTile> _currentHandTiles;
    private Coroutine _currentGenerateRoutine;

    private void Awake()
    {
        _currentHandTiles = new();
    }

    private void Start()
    {
        _currentGenerateRoutine = StartCoroutine(Generate());
    }

    private void OnValidate()
    {
        if (Application.isPlaying) return;
        _currentGenerateRoutine = StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        yield return null;


        var root = document.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(styleSheet);


        var deckContainer = Create("deck-container","bordered-box");

        var cardOne = Create<Button>("card","bordered-box");
        CardOneUI(cardOne);
        deckContainer.Add(cardOne);

        var cardTwo = Create<Button>("card","bordered-box");
        CardTwoUI(cardTwo);
        deckContainer.Add(cardTwo);

        var cardThree = Create<Button>("card","bordered-box");
        CardThreeUI(cardThree);
        deckContainer.Add(cardThree);

        root.Add(deckContainer);
    }

    private void CardOneUI(Button cardOne)
    {
        cardOne.clicked += () => TileFromHandSelected(0);

        var cardOneText = Create<Label>();
        if(_currentHandTiles.Count != 0)    
            UpdateCardText(cardOneText,_currentHandTiles[0].Name);

        
        if(tileDeckController.GetSelectedTileValue() == 0)
        {
            //make a selected card class in style sheet
            cardOne.style.backgroundColor = Color.yellow;
        }

        cardOne.Add(cardOneText);

    }

    private void CardTwoUI(Button cardTwo)
    {
        cardTwo.clicked += () => TileFromHandSelected(1);

        var cardTwoText = Create<Label>();
        if(_currentHandTiles.Count != 0)    
            UpdateCardText(cardTwoText,_currentHandTiles[1].Name);

        
        if(tileDeckController.GetSelectedTileValue() == 1)
        {

            cardTwo.style.backgroundColor = Color.yellow;
        }
        cardTwo.Add(cardTwoText);
    }

    private void CardThreeUI(Button cardThree)
    {
        cardThree.clicked += () => TileFromHandSelected(2);

        var cardThreeText = Create<Label>();
        if(_currentHandTiles.Count != 0)    
            UpdateCardText(cardThreeText,_currentHandTiles[2].Name);

        
         
        if(tileDeckController.GetSelectedTileValue() == 2)
            cardThree.style.backgroundColor = Color.yellow;
        
        cardThree.Add(cardThreeText);

    }


    private void UpdateCardText(Label card, string data)
    {
        card.text = data;
    }


    private VisualElement Create(params string[] classNames)
    {

        return Create<VisualElement>(classNames);
    }

    T Create<T>(params string[] classNames) where T : VisualElement, new()
    {
        var element = new T();

        foreach(var name in classNames){
            element.AddToClassList(name);
        }

        return element;
    }

    public void UpdateCurrentHandTiles(List<BaseTile> handTiles)
    {
        _currentHandTiles.Clear();
        for(int i = 0; i < handTiles.Count; i++)
        {
            _currentHandTiles.Add(handTiles[i]);
        }
        RestartGenerateRoutine();
    }

    public void RestartGenerateRoutine()
    {
        StopCoroutine(_currentGenerateRoutine);
        _currentGenerateRoutine = StartCoroutine(Generate());
        
    }

}
