# Document Manager
This project is an experiment with ASP.NET 5, Vue 3, and Bootstrap 5.
This project is an implementation of a document manager that allows a user to: 

1. Upload files. Uploaded files are stored in the `wwwroot/documents` directory relative to the project itself.
2. Download uploaded files.
3. Delete uploaded files.

To get started with this project, you must have several prerequisites installed.
In addition, you must follow specific steps in order to use this project.
Finally, for the purpose of 

- [Prerequisites](#prerequisites)
- [Running the Project](#running-the-project)

### Prerequisites
This project relies on .NET 5.
.NET 5 can be downloaded from [here](https://dotnet.microsoft.com/download/dotnet/5.0).

The other technologies listed above are referenced via a Content Delivery Network (CDN).
For that reason, there is nothing to do related to them.
However, there's a bit more required to run the project.

### Running the Project
To run the project, please do the following:

**Clone this repository**

`git clone https://github.com/chadcampbell/document-manager.git`

**Open a Terminal window**


**Navigate to the `src` directory**

**Restore the project dependencies**

`dotnet restore`

**Run the project**

`dotnet run`

If you intend to edit the code, you may want to run the code using `dotnet watch run` instead.