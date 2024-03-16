using System;

public class MiscEvents
{
    public event Action onCoinCollected;
    public void CoinCollected()
    {
        if (onCoinCollected != null)
        {
            onCoinCollected();
        }
    }
    public event Action<int> onWoodCollected;
    public void WoodCollected(int woodCount) // Modify the method to accept woodCount as an argument
    {
        onWoodCollected?.Invoke(woodCount);
    }

    public event Action onGemCollected;
    public void GemCollected()
    {
        if (onGemCollected != null)
        {
            onGemCollected();
        }
    }
}
