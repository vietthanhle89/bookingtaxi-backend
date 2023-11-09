﻿db = connect("mongodb://localhost:27017/BookingTaxi");


db.createCollection('Role');
db.createCollection('DriverStatus');
db.createCollection('Account');
db.createCollection('Driver');
db.createCollection('Customer');
db.createCollection('Administrator');
db.createCollection('Supporter');

db.createCollection('DocumentationImage');
db.createCollection('DriverCar');
db.createCollection('CarType');


db.Role.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "Name": "DRIVER",
        "Deleted": false
    },
    {
        "_id": ObjectId("000000000000000000000002"),
        "Name": "ADMIN",
        "Deleted": false
    },
    {
        "_id": ObjectId("000000000000000000000003"),
        "Name": "CUSTOMER",
        "Deleted": false
    },
    {
        "_id": ObjectId("000000000000000000000004"),
        "Name": "SUPPORTER",
        "Deleted": false
    }
])

db.DriverStatus.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "Name": "TAKING_BREAK",
        "Deleted": false
    },
    {
        "_id": ObjectId("000000000000000000000002"),
        "Name": "READY",
        "Deleted": false
    },
    {
        "_id": ObjectId("000000000000000000000003"),
        "Name": "ON_THE_GO",
        "Deleted": false
    }
])

db.CarType.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "Name": "4-seat car",
        "PricePerKm": 11000
    },
    {
        "_id": ObjectId("000000000000000000000002"),
        "Name": "7-seat car",
        "PricePerKm": 12500
    }
])



db.Account.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "Email": "driver@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Elsa",
        "LastName": "Tran",
        "RoleID": "000000000000000000000001",
        "ProfileImage": "000000000000000000000001.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09"
    },
    {
        "_id": ObjectId("000000000000000000000002"),
        "Email": "admin@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Elsa",
        "LastName": "Tran",
        "RoleID": "000000000000000000000002",
        "ProfileImage": "000000000000000000000002.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09"
    },
    {
        "_id": ObjectId("000000000000000000000003"),
        "Email": "customer@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Elsa",
        "LastName": "Tran",
        "RoleID": "000000000000000000000003",
        "ProfileImage": "000000000000000000000003.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09"
    },
    {
        "_id": ObjectId("000000000000000000000004"),
        "Email": "supporter@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Elsa",
        "LastName": "Tran",
        "RoleID": "000000000000000000000004",
        "ProfileImage": "000000000000000000000004.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09"
    }
])



