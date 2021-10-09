# Microservices Architecture
### 1. Course Overview
-   What are Microservices?
-   Solution Architecture
-   Application Architecrure
### 2. Creating first project, PostsService
<p style="padding:20px">
In this video we are going to create our first project called PostsService. This project is a WebAPI and we will scaffold it, then add models,Creating DbContext and then we will create PostsRepository, also we need some data to test our endpoints and we will create a class to seed data. Then we will add DTOs and for the last step we will add PostsController and it's actions
</p>

### 3. Docker & Kubernetes
<p style="padding:20px">
In this module we are going to get basic knowledges of Docker and we will containerize our Posts project then we will push the docker image to Docker Hub.  
Next I will talk about Kubernetes and then deploy Posts service to Kubernetes.  
</p>

### 4. Creating Users Service
<p style="padding:20px">
In this module we are going to create our second project called Users to store users data, after scaffolding we will add a HTTP Client to make a comminucation between Posts and Users service. Next we will containerize project and deploy it to Kubernetes. For the last step we will add an API Gateway.
</p>

### 5. Updating Posts service to use SQL Server
<p style="padding:20px">
In this module we are going to update our Posts service to use SQL Server instead of InMemoryDatabase, for security reason we will add a Kubernetes secret, Then we will deploy SQL Server to Kubernetes.
</p>

### 6. Multi-Resource API
<p style="padding:20px">
This module is not really about Microservices but more about REST, In this module we are going to complete Users service to store Users data and also minimum data of Posts like Id and Title.  There is a Microservice problem which is about making comminucation between services and in our sample we need to store Posts data in Users database because we need to get list of a user posts and if we don't use this solution we must make a call to Posts service for each user to get its posts!
<p>