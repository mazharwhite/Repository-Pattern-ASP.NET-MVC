# Repository Pattern Database First (Template)
A template to use for Repository Pattern Database First <br/>

## Architecture Division
I have divided the project architecture to four projects which includes,
1) Web project
2) Services
3) Infrasture
4) DAL

Let's look at each of the divison and see what they are.

## 1) Web/Web Api
This the main project that is published. This can be either a web project or an web api project or both. This project contains the client side and all the configurations that are required to establish connection with the database and the packages dependency.

For this repos, I created the default MVC project. However this is just for a template, anyone can modify it as they like.

## 2) Services
The next divison of the architecture is the Services project. This acts as the middle man between the DAL (Data Access Layer) Project and the Web Project.

## 3) Infrastructure
This project contains the actual entity for the database. The entity is stored in the Entity folder. The entity/databse that is used is the Database First Approach. But this can also be used for Code First Approach as well. This project will also contains the ViewModels. The ViewModels are stored in the folder called ViewModels. I structered it accordinly to how I like it, but can be organized how you like it to be.

(To Be Updated)
