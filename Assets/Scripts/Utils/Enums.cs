public enum CandleSize
{
    Small,
    Tall,
    
}

public enum CandleGirth
{
    Slim,
    Fat,
    
}

public enum EnigmaEnum
{
    Taquin,
    TaquinMiddle,
    Potion,
    PotionOnBody,
    Candles,
}


public enum DirectionEnum
{
    Up,
    Right,
    Down,
    Left,
    None,
}

[System.Serializable]
public struct CandleProperties
{
    public CandleSize candleSize;
    public CandleGirth candleGirth;

    public bool Match(CandleProperties other)
    {
        return candleSize == other.candleSize && candleGirth == other.candleGirth;
    }
}