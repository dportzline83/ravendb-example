# ravendb-example
A sample ASP.NET 5 application using RavenDB

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

## Document Databases
- Domain objects consolidated into fewer documents
- Denormalization
- Less foreign key relationships

## How to think about it
- Focus less on how the data is stored, and more on how it will be accessed
- Ask yourself, will this type ever need to be accessed independently, or does it only make sense within the context of some parent document?

# Some highlighted features of RavenDB 3
- New Raven Studio! No more Silverlight!!!
- Support for Voron storage engine (port of Lightning Memory-mapped Database)
  - More performant
  - No DTC transaction support
- Some changes to index performance
- ```WhatChanged``` and ```HasChanges``` available on session
- Missing properties on save operation are retained in database

# ACID/BASE - see links below

### Links
- http://ravendb.net/docs/article-page/2.5/csharp/theory/document-structure-design
- http://ayende.com/blog/4466/that-no-sql-thing-modeling-documents-in-a-document-database
- http://ravendb.net/docs/article-page/2.5/csharp/client-api/advanced/transaction-support
- http://ayende.com/blog/164066/ravendb-acid-base
- http://ravendb.net/docs/article-page/3.0/Csharp/start/whats-new
- http://ravendb.net/docs/article-page/3.0/csharp/server/configuration/configuration-options
- http://docs.mongodb.org/manual/core/data-modeling-introduction/
