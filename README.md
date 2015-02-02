# ravendb-example
A sample ASP.NET 5 application using RavenDB


# Some thoughts on document database design

## Traditional RDBMS
- Each "type" gets its own table
- Normalization
- Lots of foreign key relationships to produce complete picture
- Many tables/relationships accessed to produce needed data for a single page

## Document Databases
- Store more related types together in the same document
- Denormalization
- Less foreign key relationships
- Fewer documents, ideally one per logical page

### How to think about it
- Focus less on how the data is stored, and more on how it will be accessed
- Ask yourself, will this type ever need to be accessed independently, or does it only make sense within the context of some parent document?

### Links
- http://ravendb.net/docs/article-page/2.5/csharp/theory/document-structure-design
- http://ayende.com/blog/4466/that-no-sql-thing-modeling-documents-in-a-document-database
