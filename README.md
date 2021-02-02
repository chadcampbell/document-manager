# Document Manager
This project is an experiment with ASP.NET 5, Vue 3, and Bootstrap 5.
This experiment is an implementation of a document manager. 
This document manager allows a user to: 

1. Upload files. Uploaded files are stored in the `wwwroot/documents` directory relative to the project itself.
2. Download uploaded files.
3. Delete uploaded files.

To get started with this project, you must have the prerequisites installed.
Once the prerequisites have been installed, you can run the project.
Each of these steps are covered in the sections listed below.

- [Prerequisites](#prerequisites)
- [Running the Project](#running-the-project)

### Prerequisites
This project relies on .NET 5.
You can download .NET 5 from [here](https://dotnet.microsoft.com/download/dotnet/5.0).

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