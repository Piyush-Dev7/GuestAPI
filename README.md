---
title: Guest API
author: Piyush-Dev7
description: Demonstration of basic web api fundamentals.
ms.author: Piyush-Dev7
ms.date: 11/07/2024
uid: guestapi/overview
---
# Overview of GuestAPI
By [Piyush Meshram]

This project demonstrates the basic fundamentals of ASP.Net core Web API project built in .Net 6 and utilizes the Guest Entity managment operations via different endpoints.

## Pre-Requisites
  •	.Net 6.0 Runtime
  •	MS-SQL server (latest recommended)
  •	Postman tool download from:- https://www.postman.com/downloads/

## GOAL
The goal of this application is to create endpoints to: 
  
  •	Implement an endpoint to AddGuest.
  •	Implement an endpoint to AddPhone for an existing Guest.
  •	Implement an endpoint to GetGuestById.
  •	Implement an endpoint to GetAllGuests.

## Validation
  •	Implement validation for the AddGuest to ensure that at least one name and a phone number are provided.
  •	Implement validation for the AddPhone to ensure that phone numbers are not duplicated.

## Logging
  •	Introduce basic logging (using, for example, Serilog) for each endpoint.

Basic HTTP Verbs used: 
## POST
This verb is used whereever a new resource is created in the application. The application demonstrate the use of POST verb which on success can return "201 Created" status or simply 200 Ok status. 

## GET 
This verb is used to fetch the desired resources from the Web API server. This usually return 200 OK status on successful ping to the server.

## API documentation and Getting started
Download or clone the repository from the default "main" branch and open the Guest API solution. Before first run, it is recommended to create a database and procure a valid connection string and update the appsettings and data-access settings file. The different files can be later used in Azure to safely configure the application's settings in the Azure Key Vault cloud without much hassle. 

Once the application is up and running user will be presented with swagger UI but lets not use the swagger and lets jump on to the more interesting tool for Web API testing "Postman". 

Download and sign in to the postman application from here: https://www.postman.com/downloads/

Once downloaded import the json collection file provided with the repository. With in the GuestAPI web API project you may find a postman-collection folder with everything configured, the only thing that is remaning to do is to click on the "send" button step wise as mentioned below: 

## Step 1: Go To folder 1. User Registration
![image](https://github.com/Piyush-Dev7/GuestAPI/assets/69745482/c4b4cca1-3422-40a1-a44b-e2c8f58044d6)

Click on the register request go to body and update the request json. 
![image](https://github.com/Piyush-Dev7/GuestAPI/assets/69745482/33095186-9905-4693-8339-78679a5f8cc2)

now Click send, a success message will appear.

### User Registration [Request and Response]
Request Details:
  Method: POST
  URL: domain/User
  Content-Type: application/json
  Sample Request JSON: {
  "name": "__API__user__name",
  "email": "__valid__email___here",
  "password": "__any___password__here__"
  }

Sample Response: Text
User registered successfully.



### Step 2: Procure Token
Request Details:
  Method: POST
  URL: domain/Token
  Content-Type: application/json
  Sample Request JSON: {
  "email": "__valid__registered__email___here",
  "password": "__correct__password__here__"
  }

Sample Response: JWT Bearer token
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImQ1MzMxZmVhLTc3MjgtNDk5YS1hNzI5LTI1OTVkMzE4MDljYyIsIm5iZiI6MTcyMDYzODc3MSwiZXhwIjoxNzIwNjQyMzcxLCJpYXQiOjE3MjA2Mzg3NzF9.oxYOplWyTbyqzQJgPZMQ3T5HGmMpcJtH5jQQOwNh39w

### Step 3: Guest API Calls - Create Guest Information in the system.
Request Details:
  Method: POST
  URL: domain/api/Guest
  Authorization: Bearer       eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImJmMmU5MjZmLWUyY2YtNDRkNy1hYTEyLTA3ODFmYWUxZjVmOSIsIm5iZiI6MTcyMDYyMDk0NSwiZXhwIjoxNzIwNjI0NTQ1LCJpYXQiOjE3MjA2MjA5NDV9.wLfXaFiyFvVbXV4kUx2QoE7quPaLh9zqQ-vYhDa2Qgs
  Content-Type: application/json
  Sample Request JSON: {
  "email": "__valid__registered__email___here",
  "password": "__correct__password__here__"
  }

Sample Response: JWT Bearer token
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImQ1MzMxZmVhLTc3MjgtNDk5YS1hNzI5LTI1OTVkMzE4MDljYyIsIm5iZiI6MTcyMDYzODc3MSwiZXhwIjoxNzIwNjQyMzcxLCJpYXQiOjE3MjA2Mzg3NzF9.oxYOplWyTbyqzQJgPZMQ3T5HGmMpcJtH5jQQOwNh39w

### Step 4: Create 
Request Details: 
Method: POST
URL: {{Domain}}/api/Guest
Authorization: Bearer       eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImJmMmU5MjZmLWUyY2YtNDRkNy1hYTEyLTA3ODFmYWUxZjVmOSIsIm5iZiI6MTcyMDYyMDk0NSwiZXhwIjoxNzIwNjI0NTQ1LCJpYXQiOjE3MjA2MjA5NDV9.wLfXaFiyFvVbXV4kUx2QoE7quPaLh9zqQ-vYhDa2Qgs
Content-Type: application/json

Request Body: 
{
  "displayTitle": "Mr",
  "firstName": "Piyush",
  "lastName": "Watson",
  "birthDate": "1994-02-07",
  "email": "pmeshram316712@gmail.com",
  "phoneNumbers": [
    "+918889345434",
    "09822928443",
    "88834934255"
  ],
  "countryCode": "IN"
}

Resposne Body:
201 Create with Guest Id [GUID is return]


###Step 5: Add Guest Phone number 
Method: POST
URL: domain/api/guest/{GUID}/addphone
Authorization:  Bearer       eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImJmMmU5MjZmLWUyY2YtNDRkNy1hYTEyLTA3ODFmYWUxZjVmOSIsIm5iZiI6MTcyMDYyMDk0NSwiZXhwIjoxNzIwNjI0NTQ1LCJpYXQiOjE3MjA2MjA5NDV9.wLfXaFiyFvVbXV4kUx2QoE7quPaLh9zqQ-vYhDa2Qgs
Content-Type: application/json

Sample Request Body: 
{
  "phoneNumber": "8999342675",
  "countryCode": "IN"
}

Sample Response Body: 
{
    "id": "7fd03c57-f29f-4b25-badf-5c593544a1bb",
    "displayTitle": "Mr",
    "firstName": "Piyush",
    "lastName": "Watson",
    "birthDate": "1989-02-02",
    "email": "piyush63@gmail.com",
    "phoneNumbers": [
        "9834242533",
        "7834245255",
        "9834293948"
    ],
    "countryCode": "IN"
}


### Step 6: Get Guest by Id
Request Details: 
Method: GET
URL: domain/api/Guest/[GUID]
Authorization:  Bearer       eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImJmMmU5MjZmLWUyY2YtNDRkNy1hYTEyLTA3ODFmYWUxZjVmOSIsIm5iZiI6MTcyMDYyMDk0NSwiZXhwIjoxNzIwNjI0NTQ1LCJpYXQiOjE3MjA2MjA5NDV9.wLfXaFiyFvVbXV4kUx2QoE7quPaLh9zqQ-vYhDa2Qgs
Content-Type: application/json


Sample Response Body: 
{
    "id": "7fd03c57-f29f-4b25-badf-5c593544a1bb",
    "displayTitle": "Mr",
    "firstName": "Piyush",
    "lastName": "Watson",
    "birthDate": "1989-02-02",
    "email": "piyush63@gmail.com",
    "phoneNumbers": [
        "9834242533",
        "7834245255",
        "9834293948"
    ],
    "countryCode": "IN"
}


### Step 7: Fetch all guests
Reuqest Details: 
Method: GET
URL: domain/api/Guest/AllGuests
Authorization:  Bearer       eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImJmMmU5MjZmLWUyY2YtNDRkNy1hYTEyLTA3ODFmYWUxZjVmOSIsIm5iZiI6MTcyMDYyMDk0NSwiZXhwIjoxNzIwNjI0NTQ1LCJpYXQiOjE3MjA2MjA5NDV9.wLfXaFiyFvVbXV4kUx2QoE7quPaLh9zqQ-vYhDa2Qgs
Content-Type: application/json

Sample Response Body: 
{
    "id": "7fd03c57-f29f-4b25-badf-5c593544a1bb",
    "displayTitle": "Mr",
    "firstName": "Piyush",
    "lastName": "Watson",
    "birthDate": "1989-02-02",
    "email": "piyush63@gmail.com",
    "phoneNumbers": [
        "9834242533",
        "7834245255",
        "9834293948"
    ],
    "countryCode": "IN"
}



#Other Details 
##Project Structure
![SolutionHierarchy](https://github.com/Piyush-Dev7/GuestAPI/assets/69745482/2174d97d-64db-4a23-9e7b-f5ae466c2b53)
Code Maintenance


This project uses unique identifier GUID to protect against route guessing attacks, JWT authentication as its mechanism is not machine dependant and leverage .Net Core' cross platform compatibility also seperation of concerns are highly worked on through out the project, as there may arrive a case where app might talk to multiple different microservices in .Net and if all the services started maintaining their own identity server or JWT authentication then that will be more difficult code to manage so separated the libraries where data access library is different than the actual API project and hence preventing API project to directly start talking to database instance. 


## **FUTURE Enhancements**
1. Migrate to .Net 8.0 (LTS)
2. Role Based Access.
3. Automation for easy business Integration.
4. Cloud publish and security enhancements.
