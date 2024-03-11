using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private UIDocument _document;
    [SerializeField] private StyleSheet _styleSheet;

    public static event Action<int> CardFromHandSelected;

    private void Start()
    {

        StartCoroutine(Generate());
    }

    private void OnValidate()
    {
        if (Application.isPlaying) return;
        StartCoroutine(Generate());
    }

    IEnumerator Generate()
    {
        yield return null;

        var root = _document.rootVisualElement;
        root.Clear();

        root.styleSheets.Add(_styleSheet);


        var deckContainer = Create("deck-container","bordered-box");

        var cardOne = Create<Button>("card","bordered-box");
        cardOne.clicked += () => CardFromHandSelected(0);
        var cardOneText = Create<Label>();
        cardOneText.text = "test";
        cardOne.Add(cardOneText);
        
        deckContainer.Add(cardOne);

        var cardTwo = Create<Button>("card","bordered-box");
        cardTwo.clicked += () => CardFromHandSelected(1);
        deckContainer.Add(cardTwo);

        var cardThree = Create<Button>("card","bordered-box");
        cardThree.clicked += () => CardFromHandSelected(2);
        deckContainer.Add(cardThree);

        root.Add(deckContainer);
    }

    private void CardOneLogic()
    {

    }

    private void CardTwoLogic()
    {

    }

    private void CardThreeLogic()
    {

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
}
