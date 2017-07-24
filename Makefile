PROJECT_FILE = ./Stone/Stone.csproj
TEST_PROJECT_FILE = ./Stone.Tests/Stone.Tests.csproj

restore:
	dotnet restore $(PROJECT_FILE)
	dotnet restore $(TEST_PROJECT_FILE)

build:
	dotnet build $(PROJECT_FILE)
	dotnet build $(TEST_PROJECT_FILE)

clean:
	dotnet clean $(PROJECT_FILE)
	dotnet clean $(TEST_PROJECT_FILE)

test:
	dotnet test $(TEST_PROJECT_FILE)