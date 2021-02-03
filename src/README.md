# Document Manager Implementation Details
This implementation of a document manager was created with the following technologies:

- [ASP.NET 5](https://dotnet.microsoft.com/download/dotnet/5.0)
- [Bootstrap 5 (Beta 1)](https://getbootstrap.com/docs/5.0/getting-started/introduction/)
- [Vue.js 3](https://v3.vuejs.org/guide/installation.html)
- [NLog](https://nlog-project.org/)

This document explains decisions made for this implementation of a document manager.
In addition, suggestions for technical future enhancements.
These details are covered in the following sections:

- [Deciding on an Implementation Approach](#deciding-on-an-implementation-approach)
- [Looking to Future Enhancements](#looking-to-future-enhancements)

## Deciding on an Implementation Approach
While creating this implementation of a document manager, several decisions were made.
This section details the decisions made at the following levels:

    - [UI](#ui)
    - [API](#api)
    - [Data](#data)

### UI
The UI of this document manager is a Progressive Single Page Application (SPA) written with ASP.NET and Vue.js 3.0.
This means that the entire UI is developed and runs within the context of a single HTML page.
Please note, this implementation does _not_ use an application bundle that loads and runs in an HTML page.
This UI implementation may be viewed as unorthdox.
However, this was a pragmatic decision based on the scope of the document manager.

Some UI implementations rely purely on server-based technologies to render and deliver a stateless UI.
However, managing state on the client-side in some cases can deliver a richer user experience.
This desire to deliver a richer user experience can result in an over-correction.
This over-correction often leads to the creation of an application bundled as bulky JavaScript files.
However, this implementation of a document manager has a narrow scope.
For that reason, I decided on the approach seen.

**The UI runs entirely in one view.**
This UI runs in a single view. 
There are no other views that a user can navigate to.
For that reason, a router like `vue-router` does not add value to this implementation.
Since a router is not in use, state management is also greatly simplified.

While a state management library like `vuex` can add value, it's not always necessary.
In the context of the scope of this implementation, a state management library would add overhead.

The fact that state management and routing was not necessary simplified the implementation.
There isn't a _need_ to create an application bundle.
While there wasn't a technical need, an application bundle could add values in other ways such as minification.
However, for this basic implementation, that addition was not included.

**Faster Initial Experience**
The initial experience runs slightly faster than a traditional bundled app.
In a traditional bundled app, additional requests are necessary for the initial experience.
The initial request cycle for a bundled app approach typically follows these steps:

1. Client requests host page (i.e. index.html, which may be located at `/`)
2. Server responds with index.html
4. Server responds with app bundle
5. index.html loads the app bundle and initializes/mounts app
4. App requests initially required data from an API (i.e. documents)

In this implementation, the inital data is written inside of the HTML on the intial request.
This approach reduces the number of requests the browser has to make.
This approach does introduce coupling. In other words, concerns are not cleanly separated.
This is acceptable as long as the app is running in a browser.
However, if a native app was necessary, several changes would be needed.
I chose this coupled approach for three reasons:

1. Demonstrate knowledge of how data flows in a web architecture
2. Demonstrate knowledge of Razor
3. Provide a great initial experience based on the scope of the Document Manager

While the UI provides a fast inital experience, this is only part of the equation.
Another part of this equation is the API.

### API
The API of this document management was written using ASP.NET Core.
This API includes three actions:

1. `upload-document`
2. `download-document`
3. `delete-document`

These three actions interact with a catalog of `Documents`.
This catalog is discussed in the [data](#data) section below.
For now, just know that a catalog of documents exists while reviewing the actions.

#### `upload-document`
The `upload-document` action accepts a file via a `POST` method.
The `POST` method was chosen because there are virtually no limits on the amount of data that can be sent with it.
Since the document manager needs to upload files, `POST` is a natural choice.
While the `PUT` method is also a viable option, `POST` is a more standard approach.

Once the file is received, the API generates a `GUID` for it.
This `GUID` is necessary to save the file to disk.
This approach was chosen because:

- Uploaded documents may have the same file name. By using a `GUID`, files with the same name can be uploaded.
- Uploaded documents may have unsafe file names. By using a `GUID`, the concern of attempting to use an unsafe filename is removed.

While the document is saved to disk using a `GUID` for the filename, the original name is preserved in the document catalog.
Once the document has been saved to the document catalog, a tailored JSON object representing the `Document` is returned to the client.
This JSON object is important because it:

- Returns the local date/time based on the server.
- Returns a friendly representation of the file size.

#### `download-document`
The `download-document` action retrieves a document from the catalog via a `GET` method.
If the requested document is not found, a 404 error is returned.
If the document is found, it's returned to client.
Notably, the `Content-Type` of the response automatically set.
This `Content-Type` is determined based on the file extension of the document file.

#### `delete-document`
The `delete-document` action removes a document from the catalog via a `DELETE` method.
The document to be deleted is identified by a `GUID`.
If the requested document is not found, a 404 error is returned.
If the document is found, it's deleted from the document catalog.

This API is a wrapper for the data itself.

### Data
The data used in this document manager consists of two main parts:

1. The catalog of documents
2. The document files.

#### The catalog of documents
The catalog of documents is available in `wwwroot/documents/index.json`. 
At the time of writing, this path was predetermined and not configurable.
Still, the catalog includes a list of document items.
Each document item includes:

- An `id` which is a `GUID` that can be used to access the file on disk.
- A `fileName` which is the original file name when the document is loaded. This is preserved so that the UI can display it.
- A `fileSize` which is the size of the file on disk.
- An `extension` which is the extension of the file uploaded file.
- An `uploadDate` which is the UTC date/time at which the file was uploaded.

While this catalog stores the metadata of each file, the actual document files are still stored.

#### The document files
The document files are stored in the `wwwroot/documents` directory.
Each file is saved using:

1. A `GUID` generated on the server
2. The file extension of the uploaded document as identified by the server.

These files are preserved to disk for later retrieval.

This is an explanation of how the data, api, and UI work together in this implementation of the Document Manager.
This basic implementation is a solid initial implementation.
However, as with most software, there are tons of additional enhancements that could be made.
Those technical enhancements are explained in the next section.

## Looking to Future Enhancements
This implementation of a document manager could benefit from a number of technical enhancements.
These technical enhancements cover areas including:

- [UI](#ui-enhancements)
- [API](#api-enhancements)
- [Document Storage](#document-storage)
- [Security](#security-enhancements)
- [Performance](#performance-improvements)

### UI Enhancements
In the UI, there are several technical improvements that could be made.
Those improvements are detailed below.

#### Add UI Virtualization
The current implementation shows all documents on the disk.
If this list grows, it may negatively impact the performance of the UI.
For that reason, infinite scrolling or paging of documents should be considered.

UI virtualization is a technique that only renders content that is visible to a user.

#### Refactor Toast Elements in a Vue Component
Vue Components are designed to let you encapsulate reusable code.
They're a way to create building blocks in an app.
The toast element is used twice, which makes it a candidate to be refactored as a component.
Due to the minimal benefit of creating a component in this specific instance, the code was not refactored to use a component.

#### Refactor Components/Views into Single File Components
This document manager was just as big of an app as I would create before moving to using single file components.
While single file components are valuable, they do introduce additional work.
If this app were to grow in complexity, or use multiple views, I would want to refactor it to use single file components.

#### Introduce a Router
In the event that this app were to grow and require mutiple "pages", I would introduce `vue-router`.
This would provide a way to separate concerns and ensure the code was manageable.
At this point, the introduce of a state management library, like `vuex` may make sense.

#### Introduce a Packager
If this app were to grow, it might make sense to use a bundler like WebPack or Snowpack.
This would provide a way to consolidate the entire document manager into a single file.
In addition, the code could be minified and uglified for improved performance.

As much as the UI could benefit from these enhancements, the API could also benefit from some enhancements.

### API Enhancements
The API is the main interface to the data.
The current API could benefit from some technical improvements including:

#### Add Entity-Specific Routes
This implementation only required one entity: Documents. 
For that reason, all endpoints were exposed behind `api`.
However, if multiple entities (for example "Documents" and "Locations") were needed, then I would have went further. 
For example, I would have added dedicated routes for each entity. 
In other words, the endpoints would be accessible via routes such as `/api/{entity}/{action}`.

#### Add Support for API Versioning
This implementation does not support versioning.
However, robust APIs that endure the test of time demand API versioning.
This is a challenge is much deeper than it sounds.

If a relational data structure is used, you often need to then version your data schemas.
You then need to map which versions of the data schemas work with specific versions of the API.
For these reasons, API versioning has not been added in this implementation.

#### Consider Adding SignalR Support
Multiple people can use this app at the same time.
It might be cool to _push_ data updates to clients.
For example, if another user adds a document, that document could be pushed to other individuals using the app and shown in their UIs.
This type of capability is possible with SignalR.

#### Check the Filename for Safety
At this time, there is very little validation performed on documents uploaded to the API.
In an effort to be cautious, the file names of the documents should be reviewed for safety.
If a file name is unsafe, the document should be be stored on the server.
Instead, an error should be returned to the client.

#### Return A Local Date/Time Relative to the User (Client)
The `upload-document` action returns document details when a document is successfully uploaded.
These details include the `uploadDate`.
This `uploadDate` is a formatted version of the `DateTime`. 
At this time, the `uploadDate` is the local date/time of the _server_.
However, it would be better to return the local date/time relative to the user.
To do this, the user's timezone could be passed to the server via JavaScript.

#### Format File Sizes Based on User Culture
Consider formatting file size number relative to a user's culture.

Beyond these API enhancements, there are enhancements that could be made when storing the documents themselves.

### Document Storage
Currently, a very primitive document storage approach is used.
Documents are stored in the `./wwwroot/documents` directory.
This screams for multiple enhancments.
Such enhancements include:

#### Make the Location of the `documents` Directory Configurable
An administrator of the document manager should be able to configure where documents are stored.
This could be as simple as relying on an `appsettings.json` value or an environment variable.
However, this approach assumes that the goal is to store files to disk.
This approach is subject to hardware failure and may not scale well horizontally.
For that reason, another file storage mechanism may be better.

#### Use a Better File Storage Mechanism. 
Cloud-based file storage services like Azure Blob Storage would be a better option than disk.
This would remove the risk of hardware failure.
In addition, scaling would be easier.

#### Consider Supporting File History (i.e. Versioning).
The document manager could be improved to allow users to upload new versions of the same document.
At that point, versioning would be required in the document storage.
Azure Blob Storage provides support for versioning.

#### Consider Increasing the Maximum Document Upload Size. 
By default, ASP.NET Core allows a maximum file size of [28.6 MB](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-5.0#kestrel-maximum-request-body-size). 
This means that documents are limited to ~28 MB in the current implementation.
However, if desired, it may make sense to increase this limit.

These API enhancements would go along way towards creating a high-quality document manager.
However, it's important to not overlook security enhancements.

### Security Enhancements
There are a variety of security enhancements that could be made to this implementation of the document manager.
These enhancements inclue:

#### Add Support for Authentication and Authorization
At this time, this implementation runs "wide-open".
There are few security checks in place.
However, adding authentication and authorization could help restrict undesired uploads. 
In addition, this information could be added to a possible file history implementation to identify who changed a document and when.

#### Integrate Anti-Forgery Tokens
At this time, the API does little in terms of security.
However, this API would benefit from supporting Anti-Forgery tokens.
This would help prevent cross-site attacks.
This may or may not be part of adding support for authentication / authorization based on the needs of the app.

#### Implement Virus Scanning on Documents
To improve security, it's recommended to scan documents when they are uploaded.
If a virus is detected, the document should not be added to the catalog.
This would reduce the risk of unintentionally spreading viruses.

#### Compare the Document Against Known Malware
To improve security, compare the document's filename against a list of known malware.
If the filename matches the name of some malware, do not add it to the catalog.
This would reduce the risk of unintentionally spreading malware.

#### Consider a Document Review Workflow
When a document is uploaded, a workflow could be started.
If this workflow was approved, the document would be added to the document manager and accessible to other users.
However, if the document was rejected, the document would be deleted.
This is an approach I used in my own startups (DivotDog and DiscDuke). 

### Performance Improvements
Multiple enhancements could be made to improve performance.
Such enhancements include:

#### Implement a Cache of the `index.json` File. 
The current implementation loads the index of documents from the `index.json` file on each request. 
However, reading and writing from disk is a relatively expensive operation. 
An in-memory or distributed caching approach would improve performance.

#### Implement Response Compression. 
This would reduce the size of the response sent from the server to the client, reducing network bandwidth. 
This can increase the performance of the app. 
Another way to reduce the bandwidth used is to minify responses.

#### Minify Resources. 
The current implementation serves resources as they are written. 
However, CSS, HTML, and JavaScript files should be minified to reduce resource file sizes. 
This can 

    1. Get the initial app to the user faster 
    2. Improve load times

    Minification can be implemented via an automation or bundling tool like Gulp, Webpack, or Snowpack.

Thank you for reading. 😀