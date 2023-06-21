public class CandleEngimaManager : Enigma 
{
    public EnigmaEnum currentEnigma;
    //TODO : faire un bouton GUI pour dire le nombre de candleHolder présent dans la scène
    public int totalCandlesHolders;
    private int currentValidCandles = 0;

    public void ValidCandlePlaced()
    {
        currentValidCandles++;
        CheckEnigmaValidation(currentEnigma);
    }

    public void RemoveValidCandle()
    {
        currentValidCandles--;
    }

    protected override bool EnigmaCondition()
    {
        return currentValidCandles == totalCandlesHolders ?  true :  false;
    }
}
