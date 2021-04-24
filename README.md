# currency

This application is built using a Clean Architecture and CQRS approach.

# General Architectural Principles

The goal of the architecture in this solution is to accelerate the development of new features.

# Error Handling

Application Layer Exceptions are handled in the `LogExceptionsBehaviour` decorator.
Using decorators for handling these exceptions means that logging takes place automatically,
and without additional developer effort.

Error responses are automatically created in the `ErrorHandlingMiddleware` class, depending on
the type of exception thrown.


# Testing

## Convention Testing

These tests are used to enforce application conventions. 
This form of testing is rarely used but provides huge benefits in
terms of enforcing coding standards and driving up quality.

The examples in this repo are relatively simple, but the method can be used
in all kind of scenarios. For example, `VersioningConventionTests.Controllers_MustSupportVersioning`
asserts that all controllers in the solution support versioning.
`MethodConventionTests.PublicMethodParameters_Count_LessThan4`
checks that public methods don't have more than three parameters.

## Postman (Newman) Tests

To run newman tests, from the root directory run:

```cd Tests/Postman```

```docker-compose build --no-cache --progress plain```

These tests are set up to run in a CI/CD pipeline (although this hasn't been tested).

## Performance (k6) Tests

To run performance tests, from the root directory run:

```cd Tests/K6```

```docker-compose build --no-cache --progress plain```

These tests are set up to run in a CI/CD pipeline (although this hasn't been tested).
