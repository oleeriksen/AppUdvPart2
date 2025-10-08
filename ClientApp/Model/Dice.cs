namespace ClientApp.Model;

public class Dice
{
    // state
    private int size;

    private int eyes;

    private Random mGenerator;

    public Dice(int size)
    {
        this.size = size;
        mGenerator = new Random();
        Roll();
    }

    public int GetEyes() => eyes;

    public void Roll()
    {
        eyes = mGenerator.Next(1, size+1);
    }


}