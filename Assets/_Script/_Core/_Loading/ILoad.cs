namespace _Script._Core._Loading
{
    public interface ILoad
    {
        float GetLoadingValue(); //0 ~ 100
        bool Complete();
    }
}