# TreeApp

TreeApp is an ASP.NET Core application that allows managing independent tree structures. It supports tree nodes with relationships and provides REST API endpoints for creating, renaming, deleting nodes, and managing trees. The application uses PostgreSQL with code-first migrations, and it logs exceptions related to API requests.

## Features

- **Node Management**: Create, rename, and delete nodes in a tree.
- **Exception Handling**: Logs all exceptions during request processing, and provides custom error messages for specific exceptions (`SecureException`).
- **API Endpoints**: Exposes RESTful API endpoints for tree and node operations.
- **Database**: Uses PostgreSQL for persistent storage and implements code-first migrations.

## Technologies Used

- **Backend**: ASP.NET Core 6/7
- **Database**: PostgreSQL (with Entity Framework Core)
- **Exception Logging**: Custom exception handling and logging for API requests.
- **Swagger**: For API documentation and testing.

## Prerequisites

Before running the application, ensure that you have the following:

- .NET 6/7 SDK or later installed.
- PostgreSQL installed or access to a PostgreSQL instance.
- Connection string for PostgreSQL in `appsettings.json`.

## Setup

1. **Clone the Repository**:

    ```bash
    git clone https://github.com/yourusername/TreeApp.git
    cd TreeApp
    ```

2. **Configure Your PostgreSQL Connection**:

   Configure your PostgreSQL connection in the `appsettings.json` file:

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Database=TreeAppDb;Username=yourusername;Password=yourpassword"
    }
    ```

   Replace `localhost`, `yourusername`, `yourpassword`, and `TreeAppDb` with your PostgreSQL connection details.

3. **Install Dependencies**:

   Run the following command to restore the project's dependencies:

    ```bash
    dotnet restore
    ```

4. **Apply Migrations**:

   To apply the initial database migrations, run:

    ```bash
    dotnet ef database update
    ```

5. **Run the Application**:

   Start the application:

    ```bash
    dotnet run
    ```

   By default, the application will be hosted at `http://localhost:5000`.

## API Endpoints

The application provides the following API endpoints:

- **POST** `/api.user.journal.getRange`: Get a range of journal records (with pagination and optional filters).
- **POST** `/api.user.journal.getSingle`: Get a single journal record by its event ID.
- **POST** `/api.user.tree.get`: Get the entire tree for a specified tree name, or create a new one if it doesn't exist.
- **POST** `/api.user.tree.node.create`: Create a new node in the tree under the specified parent node.
- **POST** `/api.user.tree.node.delete`: Delete a node from the tree.
- **POST** `/api.user.tree.node.rename`: Rename an existing node in the tree.

## Exception Handling

The application uses a custom `SecureException` class to handle specific errors, such as when:

- A node has children and cannot be deleted.
- A node's name is duplicated in the tree.
- A parent node doesn't exist in the tree.

When such exceptions are thrown, they are logged, and the application returns a detailed error response in the following format:

- **SecureException**:

    ```json
    {
      "type": "Secure",
      "id": "event_id",
      "data": {
        "message": "Specific error message"
      }
    }
    ```

- **Other Exceptions**:

    ```json
    {
      "type": "Exception",
      "id": "event_id",
      "data": {
        "message": "Internal server error ID = event_id"
      }
    }
    ```

## Testing

You can test the API using Swagger UI. After running the application, navigate to: http://localhost:5000/swagger
