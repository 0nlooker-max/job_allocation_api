# Job Allocation API

This is a simple ASP.NET Core Web API for managing job positions with CRUD operations.

## Getting Started

1. Clone the repository
2. Navigate to the project directory
3. Run the following command to start the API:
   ```
   dotnet run
   ```
4. The API will be available at `http://localhost:5041`

## API Endpoints

### Get all job positions
```
GET /api/jobpositions
```

### Get a specific job position
```
GET /api/jobpositions/{id}
```

### Create a new job position
```
POST /api/jobpositions
```

Payload:
```json
{
  "id": 1,
  "name": "Software Engineer",
  "beginningSalary": 75000.00
}
```

### Update an existing job position
```
PUT /api/jobpositions/{id}
```

Payload:
```json
{
  "id": 1,
  "name": "Senior Software Engineer",
  "beginningSalary": 95000.00
}
```

### Delete a job position
```
DELETE /api/jobpositions/{id}
```

## Data Model

The API manages job positions with the following properties:

- `id` (int): Unique identifier for the job position
- `name` (string): Name of the job position
- `beginningSalary` (decimal): Starting salary for the position

## Technologies Used

- ASP.NET Core 8.0
- Entity Framework Core (In-Memory Database)
- Swagger/OpenAPI for API documentation