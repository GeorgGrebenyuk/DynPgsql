# DynPgsql
There is small converter from Autodesk Dynamo environmental (data and geometry) to PostgreSQL with PostGIS extension. The result of script is creation SQL-file to next importing in database.


## External dependencies*
DynamoVisualProgramming.ZeroTouchLibrary,DynamoVisualProgramming.DynamoServices - v 2.12.0.5650 

*Note: all of dependencies already configured in project's properties and downloadings via Nuget package manager.*

# About Using
**Attention**: User need create some database in PostgreSQL and run `postgis.sql` from `\contrib\postgis-3.2\`

## Uploading to Dynamo
Select latest version from  Releases and next (in Dynamo environmenta): ```File -> Upload lirary -> select DynPgsql.dll```

## Sample workings
Look script demo-1_table.with.points.dyn [from that Release](https://github.com/GeorgGrebenyuk/DynPgsql/releases/tag/v1.0.1).
![sample_screen](/docs/image-1.png).
