---
layout: post
title: "First Blog Tests"
date: 2019-02-26
---

My first blog.
Some extra.

Whoop, whoop!

Testing with some C# code.
```csharp
Person sherlock = new PersonBuilder()
    .WithName("Sherlock  Holmes")
    .IsMan()
    .WithAddress(
        new AddressBuilder()
            .WithAddressLine1("221B Baker Street")
            .WithAddressLine2("London")
            .Build()
    )
    .Build();
```


```csharp
Person sherlock = A.Man.Called("Sherlock  Holmes").LivingAt("221B Baker Street, London");
```