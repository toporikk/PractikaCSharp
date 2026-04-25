namespace ThreadErrorApp
{
    public class ThreadManager
    {
        private readonly ThreadWorker _worker = new ThreadWorker();

        public void ExecuteWork()
        {
            try
            {
                _worker.StartThread(() => { });
            }
            catch (Exception ex)
            {
                throw new ThreadOperationException("Менеджер потоков зафиксировал сбой в работе воркера.", ex);
            }
        }
    }
}
