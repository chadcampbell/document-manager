# Document Manager
This project is an experimental implementation of a document manager. 
This document manager allows a user to: 

1. Upload files. Uploaded files are stored in the `wwwroot/documents` directory relative to the project itself.
2. Download uploaded files.
3. Delete uploaded files.

To get started with this project, you must have the prerequisites installed.
Once the prerequisites have been installed, you can run the project.
Each of these steps are covered in the sections listed below.

- [Prerequisites](#prerequisites)
- [Running the Project](#running-the-project)
- [Implementation Details](#implementation-details)
- [Future Functional Enhancements](#future-functional-enhancements)

### Prerequisites
This project uses .NET 5.
You can download .NET 5 from [here](https://dotnet.microsoft.com/download/dotnet/5.0).

### Running the Project
To run the project, please do the following:

**Clone this repository**

`git clone https://github.com/chadcampbell/document-manager.git`

**Open a Terminal window**
You can use the Terminal window of your choise.

**Navigate to the `src` directory**

**Restore the project dependencies**

`dotnet restore`

**Run the project**

`dotnet run`

If you intend to edit the code, you may want to run the code using `dotnet watch run` instead.

**Navigate to `https://localhost:5001/`**
In your web browser, enter `https://localhost:5001/` into the address bar.
Please note, this implementation has only been tested in Google Chrome.

### Implementation Details
Additional information about the implementation can be found in the appropriate directory.
This implementation includes two directories with additional details:

- [src](./src/README.md) - This README includes implementation details about the source code itself.
- [test](./test/README.md) - This README includes details about the automated tests in the project.

While the current implementation is functional, there are future enhancements this document manager could benefit from.

### Future Functional Enhancements
There are enhancements that can be made from a user experience perspective.
These enhancements include, but are not limited to:

#### Add the ability to search documents. 
A basic search implementation could rely solely on the file name. 
A more advanced search implementation could index file content. 
Scoring could also be used to promote more recently uploaded documents.


#### Add the ability to filter documents. 
A basic document filter would allow users to filter based on metadata such as:
    
    - File Types
    - File Sizes
    - Upload Date

    A more advanced implementation could group metadata into more friendly options. 
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

#### Add the ability to view documents in different ways. 
For example, add the option to toggle between different views such as: 

- An icon view
- A list view 
- A table view 
- A card view.

#### Add the ability to group documents. 
Documents could be grouped by:
    - Type
    - First letter of the file name
    - Upload Date
    - File Size

#### Add the ability to edit file details. 
For example, let a user:
    - Change the file name
    - Add tags for categorization purposes
    - Commenting.
    - Revisit previous versions

#### Add an ability to preview the file.

#### Allow a user to drag a file into the file manager and automatically start the file upload process.

#### Consider restricting uploads to specific types if it makes sense.

Thank you for reading. 😀