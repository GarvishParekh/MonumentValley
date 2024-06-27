public interface ICanvasAnimation
{
    public void StartingAnimation();
    public void ClosingAnimation();
    public void UpdateAnimation();
    public void ResetElements();
    public void LevelFailed();
}

public interface IOneClickAnimation
{
    public void Animate();
}

public interface IBlockAnimation
{
    public void PlayAnimation();

    public void RewindAnimation();
}