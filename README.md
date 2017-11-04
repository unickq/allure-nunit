# Allure NUnit adapter
NUnit3 adapter for Allure Framework 

[![Build status](https://ci.appveyor.com/api/projects/status/5nomj0qw25bo8gnv?svg=true)](https://ci.appveyor.com/project/unickq/allure-nunit)

### Installation and Usage
- Set target framework **.NET 4.6.1** and higher (because of [allure-csharp](https://github.com/allure-framework/allure-csharp))
- Download from Nuget with all dependencies
- Use Allure attributes for Tests and TestFixtures

[![NuGet](http://flauschig.ch/nubadge.php?id=NUnit.Allure)](https://www.nuget.org/packages/NUnit.Allure)


### Code example:

```cs
[TestFixture]
[AllureFixture("Description for allure container")]
public class Tests
{
    [Test]
    [AllureTest("I'm a test")]
    [AllureTag("NUnit","Debug")]
    [AllureIssue("GitHub#1", "https://github.com/unickq/allure-nunit")]
    [AllureSeverity(AllureSeverity.Critical)]
    [AllureFeature("Core")]
    public void EvenTest([Range(0, 5)] int value)
    {
        Assert.IsTrue(value % 2 == 0, $"Oh no :( {value} % 2 = {value % 2}" );
    }
}
```    

### Allure report:

![Allure report](https://raw.githubusercontent.com/unickq/allure-nunit/master/AllureScreen.png)
