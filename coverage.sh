#!/bin/bash

# Install OpenCover and ReportGenerator, and save the path to their executables.
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.7.922 OpenCover
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.3.6 ReportGenerator

OPENCOVER=$PWD/packages/OpenCover.4.7.922/tools/OpenCover.Console.exe
REPORTGENERATOR=$PWD/packages/ReportGenerator.4.3.6/tools/netcoreapp2.0/ReportGenerator.dll

coverage=./coverage
mkdir $coverage

echo "Calculating coverage with OpenCover"
$OPENCOVER \
  -target:"c:\Program Files\dotnet\dotnet.exe" \
  -targetargs:"test ./Stone.Tests/Stone.Tests.csproj" \
  -output:$coverage/coverage.xml \
  -oldStyle \
  -filter:"+[Stone*]* -[Stone.*Tests*]*" \
  -register:user

echo "Generating HTML report"
dotnet $REPORTGENERATOR \
  -reports:$coverage/coverage.xml \
  -targetdir:$coverage \
  -verbosity:Error
