$currentYear = (Get-Date).Year
$slnName = "AdventOfCode.$currentYear"
$solutionProjectName = "Solution"
$testProjectName = "Test"
$srcRoot = "src"

$srcPath = "$srcRoot\$currentYear"
$slnPath = "$srcPath\$slnName.sln"


# Create src root directory if it doesn't exist
if (-not (Test-Path -Path $srcRoot)) {
    Write-Host "Creating src root directory $srcRoot"
    New-Item -ItemType Directory -Name $srcRoot
}

# Create year directory inside src if it doesn't exist
if (-not (Test-Path -Path $srcPath)) {
    Write-Host "Creating year directory $srcPath"
    New-Item -ItemType Directory -Name $srcPath
}

# Create solution file if it doesn't exist
if (-not (Test-Path -Path $slnPath)) {
    Write-Host "Creating solution file $slnPath"
    dotnet new sln -o src/$currentYear -n $slnName
}

for ($i = 1; $i -le 24; $i++) {
    $folderName = "Day{0:D2}" -f $i
    $fullPath = "$srcPath\$folderName"
    
    if (Test-Path -Path $fullPath) {
        Write-Host "Folder $fullPath already exists. Skipping..."
        continue
    }

    New-Item -ItemType Directory -Name $fullPath
    Set-Location -Path $fullPath

    # Create README.md file
    $readmeContent = @"
# $folderName
This folder contains the solution for Day $i.

## Problem

## Run the solution
To run the solution, execute the following command:

```shell
dotnet run --project ../$solutionProjectName/$solutionProjectName.csproj
```
    
## Run the tests
To run the tests, execute the following command:

```shell
dotnet test ../$testProjectName/$testProjectName.csproj
```
"@
    $readmeContent | Out-File -FilePath "README.md" -Encoding utf8
    
    # Create .NET console application & add project to solution
    $solutionPath = "$solutionProjectName\$solutionProjectName.csproj"
    dotnet new console -n "$solutionProjectName"
    dotnet sln "../$slnName.sln" add "$solutionPath"

    # Create test project & add project to solution
    $testPath = "$testProjectName\$testProjectName.csproj"
    dotnet new xunit -n "$testProjectName"
    dotnet add "$testPath" package FluentAssertions
    dotnet add "$testPath" reference "$solutionPath"
    dotnet sln "../$slnName.sln" add "$testPath"
    
    Set-Location -Path "../../.."
}

git add .