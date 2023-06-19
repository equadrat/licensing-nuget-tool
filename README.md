# Introduction
This project is an example of how to use the [equadrat licensing](https://www.nuget.org/profiles/equadrat).

The project is a CLI to sign and validate licenses.
Primary usecase is to automate work with license files, i.e. in build and release pipelines.

Internally it uses the [Portable.BouncyCastle](https://www.nuget.org/packages/Portable.BouncyCastle) to interact with keys and license files.

# Getting Started
dotnet tool install --global equadrat.Licensing.CLI
equadrat.licensing --help

# Build and Test
Open **/equadrat.Licensing.sln** in Visual Studio 2022 or later.
- *Source/CLI* is the startup project.
- *Tests/UnitTests* is the test project.

# Contribute
You can contact me on my website: [www.equadrat.net](https://www.equadrat.net/en/contact/)

# License
Please respect the license and check **equadrat.Licensing.License.md** before using the source code.