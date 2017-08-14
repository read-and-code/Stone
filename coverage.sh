#!/bin/bash

# Install OpenCover and ReportGenerator, and save the path to their executables.
nuget install -Verbosity quiet -OutputDirectory packages -Version 4.6.519 OpenCover
nuget install -Verbosity quiet -OutputDirectory packages -Version 2.5.8 ReportGenerator

OPENCOVER=$PWD/packages/OpenCover.4.6.519/tools/OpenCover.Console.exe
REPORTGENERATOR=$PWD/packages/ReportGenerator.2.5.8/tools/ReportGenerator.exe

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
$REPORTGENERATOR \
  -reports:$coverage/coverage.xml \
  -targetdir:$coverage \
  -verbosity:Error
