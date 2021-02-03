# Document Manager Testing

The document manager has been tested in a number of ways.
This document explains the current testing methods used.
In addition, other testing options are also discussed.

## Current Testing Methods
The methods used for testing vary based on the application layer.
The UI and API have been tested in the current implementation.

### UI 
The UI was tested using manual testing.
Manual testing of a UI is a great way to gradually refine the user experience.
This works because, as an engineer, you experience the pain associated with edge cases before customers do. 
This has the effect of small, incremental improvements rapidly getting applied.

The downside is that manual testing is a poor way to perform regression testing of a UI.
When regression testing of a UI is warranted, I choose to use [Webdriver I/O](https://webdriver.io/). 
This framework has built-in support for popular JavaScript testing frameworks like [Jasmine](https://jasmine.github.io/). 
In addition, Webdriver I/O supports Safari, which not all tools do.

### API
The API was tested using automated tests via Postman.
The collection of tests can be found [here](./api/PostmanCollection.json).
Postman has a built-in collection test runner.
This test runner can be run via 1) The Postman UI or 2) Command Line interface via Newman. 
The latter allows for integration with CI/CD pipelines.

While two layers of the app have been tested, more can be done.
For that reason, the following section explains additional testing options.

## Additional Testing Options 
Testing an application is a large and complex task.
If the document manager were to move to a production environment, it would benefit from the following types of tests:

- End-to-End Tests: This was hinted at in the [UI](#ui) section above. Webdriver I/O provides the ability to deliver great end-to-end tests.
- Performance Testing: Performance testing could be implemented at the UI and API levels to ensure specific goals are met. Performance tests ensure that performance is not regressing unless necessary.
- Security Testing: This would be necessary if the document manager became a production level app.
- Load Testing: Load tests could be created using JMeter.

Thank you for reading. 😀