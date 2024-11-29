param ( 
[int]$year,
[int]$day
)

$slnName = "AdventOfCode.$year"
$solutionProjectName = "Solution"
$testProjectName = "Test"
$srcRoot = "src"

$srcPath = "$srcRoot\$year"
$slnPath = "$srcPath\$slnName.sln"
$folderName = "Day{0:D2}" -f $day
$fullPath = "$srcPath\$folderName"

#if ((Get-Date) -lt (Get-Date -Year $year -Month 12 -Day 01) -or (Get-Date) -lt (Get-Date -Year $year -Month 12 -Day 25))
#{
#    Write-Host "Advent of Code $year has not started yet. Please try again on December 1st."
#    exit 1
#}

# Create src root directory if it doesn't exist
if (-not (Test-Path -Path $srcRoot))
{
    Write-Host "Creating src root directory $srcRoot"
    New-Item -ItemType Directory -Name $srcRoot
}

# Create year directory inside src if it doesn't exist
if (-not (Test-Path -Path $srcPath))
{
    Write-Host "Creating year directory $srcPath"
    New-Item -ItemType Directory -Name $srcPath
}

# Create solution file if it doesn't exist
if (-not (Test-Path -Path $slnPath))
{
    Write-Host "Creating solution file $slnPath"
    dotnet new sln -o src/$year -n $slnName
}

if (Test-Path -Path $fullPath)
{
    Write-Host "Folder $fullPath already exists."
    exit 1
}

New-Item -ItemType Directory -Name $fullPath
Set-Location -Path $fullPath

# Create README.md file
$readmeContent = @"
# [Advent of Code $year - Day $day](https://adventofcode.com/$year/day/$day)
This folder contains the solution for Day $day.

## Problem
To be added...

## Solution
To be added...

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

git add .