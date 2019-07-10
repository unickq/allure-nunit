### Allure NUnit Speps:

```cs
[TestFixture]
[AllureNUnit]
public class Tests
{
	[Test]
    public void Test()
    {
        Hello();
        DoSomething();
    }

    [AllureStep]
    public void Hello()
    {
        //Step name = Hello
    }

    [AllureStep("This method does something useful")]
    public void DoSomething()
    {
        //Step name = This method does something useful
    }
}
```  