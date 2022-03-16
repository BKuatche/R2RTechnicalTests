using Microsoft.Extensions.Configuration;

namespace R2RTechnicalTests.Config
{
    public interface IConfiguration
    {
        string this[string key] { get;}
        IConfigurationRoot Configuration { get; }
        T GetValue<T>(string key);
        string GetDocumentsFullPath(string documentName);
    }
}
