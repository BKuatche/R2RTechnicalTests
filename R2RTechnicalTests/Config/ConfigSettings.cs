using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Reflection;

namespace R2RTechnicalTests.Config
{
    public class ConfigSettings : IConfiguration
    {

        private const string TestData = nameof(TestData);

        public string this[string key] => this.Configuration[key];

        public IConfigurationRoot Configuration { get; }

        public string GetDocumentsFullPath(string documentName) =>
            Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), TestData, documentName);

        public T GetValue<T>(string key)
        {
            return (T) Convert.ChangeType(this[key], typeof(T));
        }
    }
}
