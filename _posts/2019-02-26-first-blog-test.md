---
layout: post
title: "First Blog Tests"
date: 2019-03-13
---

I've been using the [Test Data Builder](http://www.natpryce.com/articles/000714.html) pattern for quite a while now. It's really improved the readability of my test automation code. I've always had the nagging feeling though that it could be better.

Inspired by a talk I saw a couple of months a go I decided to refactor some of my test data builders. In this post I'll go through some of the steps I've taking to improve my test data builders.

The code example below is my initial take on a builder to create a simple Person object with a name, gender and address.

```csharp
Person sherlock = new PersonBuilder()
    .WithName("Sherlock Holmes")
    .IsMan()
    .WithAddress(
        new AddressBuilder()
            .WithAddressLine1("221B Baker Street")
            .WithAddressLine2("London")
            .Build()
    )
    .Build();
```

Step 1 is to move the instantiation of the PersonBuilder. I'm not really interested in the fact that I'm using a builder. I want 'A Person'. We can combining the [Object Mother](https://martinfowler.com/bliki/ObjectMother.html) pattern with the Test Data Builder pattern to make this better.  
I've created a factory class called `A`. It has a static `Person` property that returns a new instance of the `PersonBuilder`.  


```csharp
Person sherlock = A.Person
    .WithName("Sherlock Holmes")
    .IsMan()
    .WithAddress(
        new AddressBuilder()
            .WithAddressLine1("221B Baker Street")
            .WithAddressLine2("London")
            .Build()
    )
    .Build();
```

We can take this one step further and create a `Man` property in the object mother. Combining `A.Person` and the `IsMan` method into one.  
Be cautious with this. Don't use more than one (business) concept like `A.ManWithName("Sherlock Holmes")`. Combining multiple build steps is what the builder is for.

```csharp
Person sherlock = A.Man
    .WithName("Sherlock Holmes")
    .WithAddress(
        new AddressBuilder()
            .WithAddressLine1("221B Baker Street")
            .WithAddressLine2("London")
            .Build()
    )
    .Build();
```

The next step is to improve the flow of the code. `WithName` and `WithAddress` are renamed to `Called` and `LivingAt`. Making the code read more like a natural language sentence.

```csharp
Person sherlock = A.Man.Called("Sherlock Holmes")
    .LivingAt(
        new AddressBuilder()
            .WithAddressLine1("221B Baker Street")
            .WithAddressLine2("London")
            .Build()
    )
    .Build();
```
The `AddressBuilder` is stil a bit ugly. The `LivingAt` is refactored to take the first and second address line in one string, seperated by a comma. The `AddressBuilder` is used inside the `LivingAt` method, parsing the address.  
Keep this kind of logic simple or you'll have test you builders too.

```csharp
Person sherlock = A.Man.Called("Sherlock Holmes")
    .LivingAt("221B Baker Street, London")
    .Build();
```

If your using explicit types instead of the `var` keyword there's one more step you can take. You can remove the call to `Build` by introducing an [implicit type conversion operator](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/implicit) in the `PersonBuilder` that converts a `PersonBuilder` to a `Person` by calling the `Build` method.

```csharp
Person sherlock = A.Man.Called("Sherlock Holmes")
    .LivingAt("221B Baker Street, London");
```

### Conclusion
By combining the [Object Mother](https://martinfowler.com/bliki/ObjectMother.html) and [Test Data Builder](http://www.natpryce.com/articles/000714.html) and refactoring you build methods to flow more like natural language, the final result reads like a sentence. Almost like a Given step in a Gherkin scenario.

The full code example can be found [here](https://github.com/ronaldbosma/ronaldbosma.github.io/examples/CleaningUpYourTestDataBuilders).