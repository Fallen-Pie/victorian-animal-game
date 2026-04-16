namespace VictorianAnimalGame.FileReader.MapReader;

public interface IMapStrategy<out TResult>
{
    void Initialize(int width, int height);
    
    void ProcessPixel(int x, int y, uint colorCode);
    
    TResult Complete();
}