db = connect("mongodb://localhost:27017/BookingTaxi");

db.createCollection('Role');
db.createCollection('Account');

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


db.Account.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "Email": "admin@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Elsa",
        "LastName": "Tran",
        "RoleID": "000000000000000000000001",
        "ProfileImage": "000000000000000000000001.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09"
    }
])



