using System;

[Serializable]
public struct TileCoordinate : IComparable<TileCoordinate>
{
    public sbyte x;
    public sbyte y;

    public TileCoordinate(sbyte x, sbyte y)
    {
        this.x = x;
        this.y = y;
    }

    public readonly int CompareTo(TileCoordinate other)
    {
        if (this.x != other.x)
        {
            return this.x - other.x;
        }
        else
        {
            return this.y - other.y;
        }
    }
}