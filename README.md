# currency

## Running the solution

### Backend

To run the backend, from the root directory run:

`docker-compose build`

`docker-compose up`

### Frontend

To run the frontend, from the root directory run:

`npm i --g http-server`

`cd Presentation/UI`

`http-server`

# Backend

This application is built using a Clean Architecture and CQRS approach.

### Error Handling

The solution relies heavily on conventions for error handling. For example, throwing a 
`ValidationException` will result in a `400` error code being produced.

Application Layer Exceptions are handled in the `LogExceptionsBehaviour` decorator.
Using decorators for handling these exceptions means that logging takes place automatically,
and without additional developer effort.

Error responses are automatically created in the `ErrorHandlingMiddleware` class, depending on
the type of exception thrown.


## Testing

### Code Coverage - 98%

### Unit Testing

Unit testing is used to cover most of the solution. There are a few components that
haven't been tested this way because of time constraints, but these would all be tested
in a production system.

### Integration Testing

Integration tests have been used to test the 'plumbing' of the system and perform basic 
checks on the endpoints.

### Convention Testing

These tests are used to enforce application conventions. 
This form of testing is rarely used but provides huge benefits in
terms of enforcing coding standards and driving up quality.

The examples in this repo are relatively simple, but the method can be used
in all kind of scenarios. For example, `VersioningConventionTests.Controllers_MustSupportVersioning`
asserts that all controllers in the solution support versioning.
`MethodConventionTests.PublicMethodParameters_Count_LessThan4`
checks that public methods don't have more than three parameters.

### Postman (Newman) Tests

Postman is used for e2e testing.

To run newman tests, from the root directory run:

```cd Tests/Postman```

```docker-compose build --no-cache --progress plain```

These tests are set up to run in a CI/CD pipeline (although this hasn't been tested).

### Performance (k6) Tests

k6 is used for performance testing.

To run performance tests, from the root directory run:

```cd Tests/K6```

```docker-compose build --no-cache --progress plain```

These tests are set up to run in a CI/CD pipeline (although this hasn't been tested).

# Frontend

The frontend is very simple and uses a combination of Semantic HTML, jQuery, and Bootstrap to operate.


