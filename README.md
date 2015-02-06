# ravendb-example
A sample ASP.NET 5 application using RavenDB

# Some highlighted features of RavenDB 3
- New Raven Studio! No more Silverlight!!!
- Support for Voron storage engine (port of Lightning Memory-mapped Database)
  - More performant
  - No DTC transaction support
- Some changes to index performance
- ```WhatChanged``` and ```HasChanges``` available on session
- Missing properties on save operation are retained in database

# Advantages of a document database over RDBMS
- Ease of development
  - No difference between data storage model and application model
  - Schemaless, so the document structure is created on the fly
  - Easier to change document structure in a rapid development workflow
- Can be more efficient if used properly
  - Documents usually stored as continguous memory blocks
  - Better for distributed database system because better locality can be maintained

# Some thoughts on document database design

## Traditional RDBMS
- Domain objects spread across many tables
- Normalization
- Lots of foreign key relationships to produce complete picture

  ```
  // Orders
  {
    "Id": "1",
    "UserId": "1"
  }
  
  // OrderLines
  {
    "Id": "1",
    "ProductId": "1",
    "Quantity": "10",
    "Price": "10.00"
  }
  
  // Products
  {
    "Id": "1",
    "Description": "Blah blah blah",
    "Price": "10:00"
  }
  ```

## Document Databases
- Domain objects consolidated into fewer documents
- Denormalization
- Less foreign key relationships

```
  // Orders
  {
    "Id": "1",
    "UserId": "1",
    "OrderLines":
    [
      {
        "Id": "1",
        "ProductId": "1",
        "Quantity": "10",
        "Price": "10.00"
      }
    ]
  }
  
  // Products
  {
    "Id": "1",
    "Description": "Blah blah blah",
    "Price": "10:00"
  }
  ```

## How to think about it
- Focus less on how the data is stored, and more on how it will be accessed
- Ask yourself, will this type ever need to be accessed independently, or does it only make sense within the context of some parent document?

# Indexes
- Queries are always against indexes
- Indexes are built asynronously in the background
- Data can be stale
- Static/Dynamic indexes
- ACID/BASE - http://ravendb.net/docs/article-page/2.5/csharp/client-api/advanced/transaction-support

# Examples of usage in this app
- This is a simple application to demonstrate basic database creation, session usage, and CRUD operations
- Start with ```DocumentStoreLifecycle``` to see how to create a database
- See the ```PeopleController``` for basic usage of Raven, including:
  - Creating
  - Editing
  - Deleting
  - Loading
  - Loading multiple docs
  - Querying with Linq
  - Some advanced querying features
  - Paging
  - Stale data

### Links
- Document design
  - http://ravendb.net/docs/article-page/2.5/csharp/theory/document-structure-design
  - http://ayende.com/blog/4466/that-no-sql-thing-modeling-documents-in-a-document-database
  - http://docs.mongodb.org/manual/core/data-modeling-introduction/
- More on ACID/BASE nature of Raven
  - http://ayende.com/blog/164066/ravendb-acid-base
- Server configuration
  - http://ravendb.net/docs/article-page/3.0/csharp/server/configuration/configuration-options
- New in RavenDB 3
  - http://ravendb.net/docs/article-page/3.0/Csharp/start/whats-new
