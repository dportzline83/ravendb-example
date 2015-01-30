using System;
using Raven.Client;
using Raven.Client.Document;

namespace RavenExample.Data
{
    public class DocumentStoreLifecycle : IDisposable
    {
        public IDocumentStore Store { get; private set; }

        public DocumentStoreLifecycle()
        {
            Store = new DocumentStore
            {
                Url = "http://localhost:8080",
                DefaultDatabase = "RavenExample"
            }.Initialize();

            AddIndexes();
        }

        private void AddIndexes()
        {
            new People_ByBirthdate().Execute(Store);
        }

        public void Dispose()
        {
            Store.Dispose();
        }
    }
}