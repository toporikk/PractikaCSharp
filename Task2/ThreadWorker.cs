namespace ThreadErrorApp
{
    public class ThreadWorker
    {
        public void StartThread(Action action)
        {
            throw new PlatformNotSupportedException("Поток был принудительно остановлен (ThreadAbort simulation).");
        }
    }
}
