## DataStoreWebAPI
### Sample data store web API based on microservices architecture
Project delivers web API to upload .csv file, convert it into logical model and persist in both MS SQL database and JSON file. API accepts streams which allows to upload large files without great memory consumption and raising time-outs. Microservices are split into 3 parts – API, Domain and Infrastructure. API layer provide web interface and preprocess the data. Domain layer implements business logic. Infrastructure layer provides access to data. Project is integrated with Docker  - microservices are run on lightweight Linux containers. So far there are 3 containers: Shop (core API logic), SQL Server (container with database) and Shop Manager (small ASP .NET Core  MVC application which provides UI)
### Requirements
- Docker Desktop  
- Docker-compose
### Usage
1. Build project using Visual Studio (19)
2. Use Visual Studio or docker-compose to run app
3. Web browser opens Shop Manager app. Use it to upload .csv file
4. After submit you will receive information about number of inserted articles
### .csv file format:
- Rows are delimited by new line character "\n"
- Columns are delimited by comma ","
- First row contains columns names
- Columns names: Key, ArtikelCode, ColorCode, Description, Price, DiscountPrice, DeliveredIn, Q1, Size, Color
- File should ends with new line character "\n"
