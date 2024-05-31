# Agri-Energy Connect Platform Prototype

Welcome to the Agri-Energy Connect Platform Prototype! This README provides detailed instructions on setting up the development environment, building and running the prototype, and understanding the system's functionalities and user roles.

## Setting Up the Development Environment

1. **Install Visual Studio**: Download and install Visual Studio from the official Microsoft website.
   
2. **Clone Repository**: Clone the repository containing the prototype code to your local machine.
   
3. **Database Setup**:
   - Design and create a relational database to manage information about farmers and products.
   - Populate the database with sample data to simulate real-world scenarios.
   
4. **Install Dependencies**: Ensure that all necessary dependencies, such as Entity Framework, are installed.
   
5. **Configure Connection String**: Update the connection string in the application configuration file (`app.config` or `web.config`) to point to your database.

## Building and Running the Prototype

1. **Open Solution**: Open the solution file in Visual Studio.
   
2. **Build Solution**: Build the solution to compile the code and resolve any dependencies.
   
3. **Run the Application**: Start the application using the debugging feature in Visual Studio or by pressing `F5`.
   
4. **Access the Prototype**: Once the application is running, access it through your web browser using the provided URL.

## System Functionalities and User Roles

The prototype demonstrates the following functionalities and user roles:

### User Roles:

- **Farmer**: Can add products to their profile and view their own product listings.
- **Employee**: Can add new farmer profiles, view all products from specific farmers, and use filters for product searching.

### Functional Features:

#### For Farmers:

- Enable product addition feature where farmers can add new products with details like name, category, and production date.

#### For Employees:

- Functionality to add new farmer profiles with essential details.
- Capability to view and filter a comprehensive list of products from any farmer based on criteria such as date range and product type.
