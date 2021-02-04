# Document Manager Testing

The document manager has been tested several ways.
This document explains the methods used to test the document manager.
In addition, other testing options are also discussed.

## Current Testing Methods
Multiple layers in the application were tests.
The methods used varied based on the layer being tested.
This section explains how the following layers were tested:

- [UI](#ui)
- [API](#api)

Please continue reading to learn more about the test approach used.

### UI 
The UI was tested using manual testing.
Manual testing of a UI is a great way to rapidly refine a user experience.
This works because, as an engineer, you experience the pain associated with edge cases before customers do. 
This has the effect of small, incremental improvements quickly being applied.

The downside is that manual testing is a poor way to perform regression testing of a UI.
When regression testing of a UI is warranted, I choose to use [Webdriver I/O](https://webdriver.io/). 
This framework has built-in support for popular JavaScript testing frameworks like [Jasmine](https://jasmine.github.io/). 
In addition, Webdriver I/O supports Safari, which not all tools do.

The UI is only one layer that was tested.
In addition, the API was tested.

### API
The API was tested using automated tests via Postman.
To run these tests, you must:

- [Setup the test environment](#setting-up-your-test-environment)
- [Run the collection of tests](#running-the-collection-of-tests)

Each of these steps are detailed in this section.

#### Setting Up Your Test Environment
To setup your test enviornment, you must:

- [Import the test collection](#importing-the-test-collection)
- [Copy the test files to the Postman Working Directory](#copying-the-test-files-to-the-postman-working-directory)

The following sections how to complete each of these steps in detail.

##### Importing the Test Collection
To import the test collection:

- Open Postman
- Import the test collection
    - Click the "Import" button in the top left of Postman
    - Click "Upload Files" from the "File" tab in the "IMPORT" modal window
    - Choose the PostmanCollection.json file in this repository. That file is available at [/test/api/PostmanCollection.json](./api/PostmanCollection.json).
    - Click "Open"
    - Click the "Import" button in the "IMPORT" modal window

Once you have imported the test collection, you should copy the test files to the Postman Working Directory.

##### Copying the Test Files to the Postman Working Directory
To copy the test files to the Postman Working Directory:

- Open Postman
- Find your Postman Working Directory
    - Open "Settings" by clicking the gear icon in the upper right and choosing "Settings"
    - Review the Working Directory Location in the "General" tab of the "SETTINGS" modal window
- Copy the [README.md](../README.md) file to _your_ Postman Working Directory
- Copy the [large-file.txt](./api/large-file.txt) file to _your Postman Working Directory

With the test files copied to the Postman Working Directory, and the test collection imported, you can run the collection of tests.

#### Running the Collection of Tests
To run the collection of tests you can use Postman's built-in collection test runner.
This test runner can be run via: 

1. The Postman UI or
2. Command Line Interface via Newman

The latter allows for integration with CI/CD pipelines. 
However, this documentation will explain how to run tests from the Postman UI.
To run the test collection from the Postman UI:

- Open Postman
- Open the test Collection Runner by clicking "Runner" in the top left of Postman
- Choose the "Document Manager" collection
- Click the blue "Run Document Man.." button.
 
This is all it takes to rapidly run a collection of tests in Postman.
While two of the layers of the app have been tested, more testing can be done.
For that reason, the following section explains additional testing options.

## Additional Testing Options 
Testing an application is a large and complex task.
If the document manager were to move to a production environment, it would benefit from the following types of tests:

- End-to-End Tests: This was hinted at in the [UI](#ui) section above. Webdriver I/O provides the ability to deliver great end-to-end tests.
- Performance Testing: Performance testing could be implemented at the UI and API levels to ensure specific goals are met. Performance tests ensure that performance is not degrading unless necessary.
- Security Testing: This would be necessary if the document manager became a production level app.
- Load Testing: Load testing could be created using JMeter.

Thank you for reading. 😀