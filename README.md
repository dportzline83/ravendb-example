# ravendb-example
A sample ASP.NET 5 application using RavenDB


# Some thoughts on document database design

## Traditional RDBMS thinking
- Each "type" gets its own table
- Normalization
- Lots of foreign key relationships to produce complete picture
- Many tables/relationships accessed to produce needed data for a single page

## Document Databases
- Store more related types together in the same document
- Denormalization
- Less foreign key relationships
- Fewer documents, ideally one per logical page
