namespace _Script._Core._Loading
{
    public interface ILoading
    {
        float GetLoadingValue(); //0 ~ 100
        bool Complete();
    }
}