# Nipr Parser #

The [National Insurance Producer Registry](https://www.nipr.com) provides a paid subscription service to their [Producer Database (PDB)](https://www.nipr.com/index_nipr_producer_database.htm). This service accepts `x-www-form-urlencoded` encoded requests and returns an XML document with the requested data.

This .NET packages serves as both a serializer/deserializer for the two types of respones (`HITLIST` and `PDB`) as well as helper methods to read/write the files to disk and extension methods to easily retrieve and manipulate the data.
