# DynPgsql
Autodesk Dynamo to PostgreSQL and transfering geometry and attributics data

## About project
Transfering model's geometry (2D, 3D) with properties to PostgreSQL with PostGIS addition. As enviromental for export are using Autodesk Design Script's library in Autodesk Dynamo Core (unification all product's Dynamo -- Revit, Civil 3D and non-Autodesk's software where is support COM-acess).

## Why is Dynamo?
Selecting Dynamo as enviromental - only my wish to combine approach to getting data in different software (that allow working with Dynamo) to prevention write libraries for each software's product.

## External dependencies
Because of I'm using Npgsql (that don't support .NET Framework) -- there are some auxiliary libraries to get compatibility with  .NET Standard lib:
1. System.Numerics.Vectors - v 4.5.0;
2. System.Runtime.CompilerServices.Unsafe - v 6.0.0;
3. System.Text.Encodings.Web - v 6.0.0;
4. System.Text.Json - v 6.0.0;
5. System.Threading.Channels - v 6.0.0;
6. System.Threading.Tasks.Extensions - v. 4.5.4;
7. System.ValueTuple - v 4.5.0;
8. Microsoft.Bcl.AsyncInterfaces - v 6.0.0
9. Microsoft.Bcl.HashCode - v 1.0.0;
10. NETStandard.Library - v 2.0.3;
11. System.Buffers - v 4.5.1;
12. System.Diagnostics.DiagnosticSource - v 6.0.0;
13. System.Memory - v 4.5.4;

Also there are technical libs:
1. Npgsql - v 6.0.3;
2. DynamoVisualProgramming.ZeroTouchLibrary,DynamoVisualProgramming.DynamoServices - v 2.12.0.5650 

*Note: all of dependencies already configured in project's properties and downloadings via Nuget package manager.*

# About Using
## Sample workings

![sample_screen](/docs/image-1.png).

## Uploading to Dynamo
Nowadays package need upload as external library (from Debug's folder in VS's project -- for debugging support). File -> Upload lirary -> selection "DynPgsql.dll"
