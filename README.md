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
- Each "type" gets its own table
- Normalization
- Lots of foreign key relationships to produce complete picture
- Many tables/relationships accessed to produce needed data for a single logical grouping

## Document Databases
- Store more related types together in the same document
- Denormalization
- Less foreign key relationships
- Fewer documents, ideally one per logical page

### How to think about it
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
