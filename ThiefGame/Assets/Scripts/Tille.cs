using System;
using System.Globalization;

public abstract class Tile {
    public bool IsNorthOpen;
    public bool IsSouthOpen;
    public bool IsEastOpen;
    public bool IsWestOpen;
    public CardType CardType;
    public delegate void EntryCardEffect();
    public delegate void EscapeCardEffect();
}