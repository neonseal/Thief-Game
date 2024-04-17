using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckUI : MonoBehaviour {
    [SerializeField] private UIDocument document;
    [SerializeField] private StyleSheet styleSheet;

    [SerializeField] private TileDeckController tileDeckController;

    public static event Action<int> TileFromHandSelected;
    public static event Action DiscardButtonPressed;

    private List<BaseTile> _currentHandTiles;
    private Coroutine _currentGenerateRoutine;

    private void Awake() {
        _currentHandTiles = new();
    }

    private void Start() {
        //Using a Coroutine because of the abilitity to do animations in the future
        _currentGenerateRoutine = StartCoroutine(Generate());
    }

    private void OnValidate() {
        if (Application.isPlaying) {
            return;
        }

        _currentGenerateRoutine = StartCoroutine(Generate());
    }

    IEnumerator Generate() {
        yield return null;


        var root = document.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(styleSheet);

        var deckContainer = Create("deck-container", "bordered-box");

        //creates the UI for the cards in hand
        for (int i = 0; i < _currentHandTiles.Count; i++) {
            var card = Create<Button>("card", "bordered-box");
            CardUI(card, i);
            deckContainer.Add(card);
        }

        root.Add(deckContainer);

        //discard button UI
        var discardContainer = Create("bordered-box", "discard-container");

        var discardButton = Create<Button>("discard-button");
        discardContainer.Add(discardButton);
        discardButton.clicked += () => DiscardButtonPressed();
        root.Add(discardContainer);

    }

    private void CardUI(Button card, int numberInHand) {
        card.clicked += () => TileFromHandSelected(numberInHand);

        var cardOneText = Create<Label>();

        if (Application.isPlaying) {
            if (_currentHandTiles?.Count != 0) {
                UpdateCardText(cardOneText, _currentHandTiles[numberInHand].Name);
            }
        }

        //changes the uss class of the selected card
        if (tileDeckController.GetSelectedTileValue() == numberInHand) {
            card.AddToClassList("selected-card");
        } else {
            card.RemoveFromClassList("selected-card");
        }

        card.Add(cardOneText);
    }


    private void UpdateCardText(Label card, string data) {
        card.text = data;
    }


    private VisualElement Create(params string[] classNames) {

        return Create<VisualElement>(classNames);
    }

    //creates UI and adds uss classes based on the entered parameters
    T Create<T>(params string[] classNames) where T : VisualElement, new() {
        var element = new T();

        foreach (var name in classNames) {
            element.AddToClassList(name);
        }

        return element;
    }

    public void UpdateCurrentHandTiles(List<BaseTile> handTiles) {
        _currentHandTiles.Clear();
        for (int i = 0; i < handTiles.Count; i++) {
            _currentHandTiles.Add(handTiles[i]);
        }
        RestartGenerateRoutine();
    }

    public void RestartGenerateRoutine() {
        if (_currentGenerateRoutine != null) {
            StopCoroutine(_currentGenerateRoutine);
        }
        _currentGenerateRoutine = StartCoroutine(Generate());

    }

}
