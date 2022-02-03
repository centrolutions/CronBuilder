# CronBuilder for .NET
A library for creating cron expressions in a user-friendly manner.

## Download & Install

Nuget Package [CronExpressionBuilder](https://www.nuget.org/packages/CronExpressionBuilder/)

```powershell
Install-Package CronExpressionBuilder
```
Minimum Requirements: **.NET Standard 2.0**.

## Usage
```csharp
var builder = new CronBuilder();
var expression = builder.Minutes(10).Hours("12-23/2").Build(); //every two hours at 10 minutes past the hour between noon and midnight
// expression = "10 12-23/2 * * *
```
### Common Expressions using ExpressionFactory
By default, serial numbers use alpha numerics (letters and numbers / Base-36), but you can change that by using a different encoder.
```csharp
var factory = new ExpressionFactory();
var expression = factory.DailyAtTime(12, 30).Build(); //every day at 12:30pm
// expression = 30 12 1/1 * *
```

```csharp
var factory = new ExpressionFactory();
var expression = factory.EveryXMinutes(15).Build(); //every 15 minutes
// expression = 0/15 * * * *
```
