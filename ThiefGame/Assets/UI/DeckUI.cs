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
    public static event Action DiscardButtonPressed;

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

        if(_currentHandTiles.Count > 0)
        {
            var cardOne = Create<Button>("card","bordered-box");
            CardOneUI(cardOne);
            deckContainer.Add(cardOne);
        }

        if(_currentHandTiles.Count > 1)
        {
            var cardTwo = Create<Button>("card","bordered-box");
            CardTwoUI(cardTwo);
            deckContainer.Add(cardTwo);
        }

        if(_currentHandTiles.Count > 2)
        {
            var cardThree = Create<Button>("card","bordered-box");
            CardThreeUI(cardThree);
            deckContainer.Add(cardThree);
        }

        root.Add(deckContainer);

        var discardContainer = Create("bordered-box","discard-container");

        var discardButton = Create<Button>("discard-button");
        discardContainer.Add(discardButton);
        discardButton.clicked += () => DiscardButtonPressed();
        root.Add(discardContainer);
    }

    //methods used to organize the ui logic
    private void CardOneUI(Button cardOne)
    {
        cardOne.clicked += () => TileFromHandSelected(0);

        var cardOneText = Create<Label>();

        if(Application.isPlaying)
        {
            if(_currentHandTiles?.Count != 0)    
                UpdateCardText(cardOneText,_currentHandTiles[0].Name);
        }
   

        
        if(tileDeckController.GetSelectedTileValue() == 0)
        {
            cardOne.AddToClassList("selected-card");
        }
        else
            cardOne.RemoveFromClassList("selected-card");

        

        cardOne.Add(cardOneText);

    }

    private void CardTwoUI(Button cardTwo)
    {
        cardTwo.clicked += () => TileFromHandSelected(1);

        //displays card name 
        var cardTwoText = Create<Label>();

        if(Application.isPlaying)
        {
            if(_currentHandTiles.Count != 0)    
                UpdateCardText(cardTwoText,_currentHandTiles[1]?.Name);
        }
       


        
        if(tileDeckController.GetSelectedTileValue() == 1)
        {
            cardTwo.AddToClassList("selected-card");
        }
        else
            cardTwo.RemoveFromClassList("selected-card");
        cardTwo.Add(cardTwoText);
    }

    private void CardThreeUI(Button cardThree)
    {
        cardThree.clicked += () => TileFromHandSelected(2);

        var cardThreeText = Create<Label>();


        if(Application.isPlaying)
        {
            if(_currentHandTiles.Count != 0)    
                UpdateCardText(cardThreeText,_currentHandTiles[2].Name);
        }

        
         
        if(tileDeckController.GetSelectedTileValue() == 2)
            cardThree.AddToClassList("selected-card");
        else
            cardThree.RemoveFromClassList("selected-card");
        
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
