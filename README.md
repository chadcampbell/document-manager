# Document Manager
This project is an experimental implementation of a document manager. 
This document manager allows a user to: 

1. Upload files. Uploaded files are stored in the `wwwroot/documents` directory relative to the project itself.
2. Download uploaded files.
3. Delete uploaded files.

To get started with this project, you must have the prerequisites installed.
Once the prerequisites have been installed, you can run the project.
These steps, and related information, are covered in the sections listed below.

- [Prerequisites](#prerequisites)
- [Running the Project](#running-the-project)
- [Implementation Details](#implementation-details)
- [Future Functional Enhancements](#future-functional-enhancements)

### Prerequisites
This project uses .NET 5.
You can download .NET 5 from [here](https://dotnet.microsoft.com/download/dotnet/5.0).

### Running the Project
To run the project, please complete the following steps:

- Open a Terminal window

    You can use the Terminal window of your choice.

- Clone this repository

    To clone this repository, run the following command from your Terminal.

    `git clone https://github.com/chadcampbell/document-manager.git`

- Navigate to the `src` directory

    Navigate to the `src` directory in your Terminal.
    You can view the contents [here](./src/) as an example.

- Restore the project dependencies

    `dotnet restore`

- Run the project

    `dotnet run`

    If you intend to edit the code, you may want to run the code using `dotnet watch run` instead.

- Navigate to `https://localhost:5001/`

    In your web browser, enter `https://localhost:5001/` into the address bar.
    Please note, this implementation has only been tested in Google Chrome.

Once you have completed these steps you should see the document manager running in your web browser. The next section includes implementation details about this document manager.

### Implementation Details
The implementation of the document manager includes details about the source code and tests.
This information is spread across multiple directories in this repository.
These details can be found in the README files in the following directories:

- [src](./src/README.md) - This README includes implementation details about the source code itself.
- [test](./test/README.md) - This README includes details about the automated tests in the project.

While the current implementation is functional, there are future enhancements this document manager could benefit from.

### Future Functional Enhancements
There are enhancements that can be made from a user experience perspective.
These enhancements include, but are not limited to, the following:

#### Add the ability to search documents 
The ability to search documents would improve the user experience.
A basic search implementation could be added using just the file name. 
A more advanced search implementation could index content of the documents. 
Scoring could also be used to promote more recently uploaded documents.

#### Add the ability to filter documents
The ability to filter documents would help users find what they're looking for faster. 
Filters could be added to allow users to refine their view based on metadata such as:
    
- File Types
- File Sizes
- Upload Date

A more advanced filter implementation could group metadata into more friendly options. 
For example:
    
- File Type categories that could be created:
    - .jpg, .gif, .bmp, .png, etc. files could be categorized as "Pictures"
    - .mov, .mp4, etc files could be categorized as "Videos"
    - .docx, .xlsx, .pdf, etc files could be categorized as "Documents"
    - .mp3, .wav, etc could be categorized as "Audio Files"
- File Size categories that could be created:
    - Small (< 1 MB)
    - Average (< 25 MB)
    - Large (< 1 GB)
    - Gigantic (>1 GB)
- Upload Date categoried that could be created:
    - This Week
    - This Month
    - Last Month
    - This Year
    - Older
    - Custom

#### Add the ability to view documents in different ways
Users may want to view documents differently based on the type of documents their managing.
For this reason, it could be helpful to add different view options such as:

- An icon view
- A list view 
- A table view 
- A card view.

#### Add the ability to group documents
Grouping documents can provided an easier managemnt experience.
For this reason, it could be helpful to enable a user to choose if they want to group the documents by:

- Type
- First letter of the file name
- Upload Date
- File Size

#### Add the ability to edit file details
The current implementation limits the user to adding and deleting documents.
However, once a document is added, a user may want to change information about it.
For example, it may be useful to let a user edit file details such as:

- Change the filename
- Add tags for categorization purposes
- Add comments to a document
- Revisit previous versions

#### Add an ability to preview the file
Sometimes, files have ambiguous names.
The ability to preview the contents of a file would enable a user to get a quick reminder of what the file is. 

#### Allow a user to drag a file into the file manager
Drag-and-drop abilities are a common feature of desktop apps.
The drag-and-drop API in JavaScript opens the doors to the ability to deliver this in this document manager.
If a document is dragged onto the surface, the file upload could automatically start.

#### Consider restricting uploads to specific types if it makes sense
The current implementation lets users upload virtually any kind of file.
However, it may be desired to restrict this to known / supported file types.
The importance of this increases if document content searching and file previews were added.

Thank you for reading. 😀