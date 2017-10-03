PROJECT_FILE = ./Stone/Stone.csproj
TEST_PROJECT_FILE = ./Stone.Tests/Stone.Tests.csproj

build:
	dotnet build $(PROJECT_FILE)
	dotnet build $(TEST_PROJECT_FILE)

test:
	dotnet test $(TEST_PROJECT_FILE)

clean:
	dotnet clean $(PROJECT_FILE)
	dotnet clean $(TEST_PROJECT_FILE)