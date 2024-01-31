namespace Core.SaveSystem.Saving.Common.Load
{
    public class LoadResult<T>
    {
        public Result Result { get; }
        public T Data { get; }

        public LoadResult(Result result, T data)
        {
            Result = result;
            Data = data;
        }
    }
    
    public enum Result
    {
        LoadedSuccessfully,
        SaveFileNotFound,
    }
}
