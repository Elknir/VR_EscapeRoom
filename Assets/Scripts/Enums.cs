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
    Potion,
    Candles,
    Powder,
    Dance,
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