using AsyncDemo;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ThreadPool.SetMinThreads(1, 1);
WebApplication app = builder.Build();

app.MapGet("/", () =>
{
    ThreadCount threadCount = new();

    ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
    threadCount.MinWorkerThreads = workerThreads;
    threadCount.MinCompletionPortThreads = completionPortThreads;

    ThreadPool.GetMaxThreads(out workerThreads, out completionPortThreads);
    threadCount.MaxWorkerThreads = workerThreads;
    threadCount.MaxCompletionPortThreads = completionPortThreads;

    return threadCount;
});

app.MapGet("GetSync", () =>
{
    Thread.Sleep(TimeSpan.FromSeconds(2));
    return "Sync Hello World!";
});
app.MapGet("GetAsync", async () =>
{
    await Task.Delay(TimeSpan.FromSeconds(2));
    return "Async Hello World!";
});
app.Run();

//https://websurge.west-wind.com/download