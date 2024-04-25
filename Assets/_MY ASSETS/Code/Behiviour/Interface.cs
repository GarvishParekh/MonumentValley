public interface ICanvasAnimation
{
    public void StartingAnimation();
    public void ClosingAnimation();
    public void UpdateAnimation();
    public void ResetElements();
}

public interface IOneClickAnimation
{
    public void Animate();
}