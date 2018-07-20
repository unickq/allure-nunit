# Allure NUnit adapter
NUnit3 adapter for Allure Framework 

[![Build status](https://ci.appveyor.com/api/projects/status/5nomj0qw25bo8gnv?svg=true)](https://ci.appveyor.com/project/unickq/allure-nunit)[![NuGet](http://flauschig.ch/nubadge.php?id=NUnit.Allure)](https://www.nuget.org/packages/NUnit.Allure)

### Allure report:

![Allure report](https://raw.githubusercontent.com/unickq/allure-nunit/master/AllureScreen.png)

### Code example:

```cs
[TestFixture]
[AllureNUnit]
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

### ToDo:
- [x] NET 4.5, NET Standard 2.0 support
- [x] Steps Wrapping - with custom method
- [x] Allure SetUp/TearDown support
- [x] Attachments
- [x] Parallelizable(ParallelScope.Fixtures)
- [x] Parallelizable(ParallelScope.Children)
- [x] Add ignored (not started) tests to results. Assert.Ignore() works :) [AllureDisplayIgnored]


### Installation and Usage
- Download from Nuget with all dependencies
- Configure allureConfig.json
- Set AllureNUnit attribute under test fixture
- Use other attributes if needed

[![NuGet](http://flauschig.ch/nubadge.php?id=NUnit.Allure)](https://www.nuget.org/packages/NUnit.Allure)