using System;
using System.Net.Http;

/// <summary>
/// Summary description for Class1
/// </summary>
public class LearnerApiHelper
{
    public HttpClient Initial()
    {
        var client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7223/");
        return client;
    }
}
