db = connect("mongodb://localhost:27017/BookingTaxi");

db.dropDatabase('BookingTaxi');

db.createCollection('Role');
db.createCollection('DriverStatus');
db.createCollection('Account');

db.createCollection('DocumentationImage');
db.createCollection('DriverCar');
db.createCollection('CarType');

db.createCollection('Booking');
db.createCollection('BookingAssignation');
db.createCollection('BookingStatus');
db.createCollection('TripRecord');



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

db.BookingStatus.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "Name": "WAITING",
    },
    {
        "_id": ObjectId("000000000000000000000002"),
        "Name": "CONNECTED"
    },
    {
        "_id": ObjectId("000000000000000000000003"),
        "Name": "CANCELLED"
    },
    {
        "_id": ObjectId("000000000000000000000004"),
        "Name": "ON THE GO"
    },
    {
        "_id": ObjectId("000000000000000000000005"),
        "Name": "COMPLETED"
    }
])

db.Account.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "Email": "driver@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Driver",
        "LastName": "4-seat car",
        "RoleID": "000000000000000000000001",
        "ProfileImage": "000000000000000000000001.jpg",
        "Deleted": false,
        "CreatedDate": "2023-11-09",
        "Phone": "0765088879",
        "Gender": "Male",
        "Address": "133 Hoàng Văn Thụ, Quận Tân Bình, TP.HCM",
        "NationalID": "079088012345",
        "DriverStatusID": "",
        "Approved": true
    },
    {
        "_id": ObjectId("000000000000000000000010"),
        "Email": "driver2@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Driver",
        "LastName": "7-seat car",
        "RoleID": "000000000000000000000001",
        "ProfileImage": "000000000000000000000010.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09",
        "Phone": "0765088879",
        "Gender": "Male",
        "Address": "133 Hoàng Văn Thụ, Quận Tân Bình, TP.HCM",
        "NationalID": "079088012345",
        "DriverStatusID": "",
        "Approved": false
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
        "Email": "customer3@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Viết Thanh",
        "LastName": "Lê",
        "RoleID": "000000000000000000000003",
        "ProfileImage": "000000000000000000000003.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09",
        "Phone": "0765000003",
        "Gender": "Male",
        "Address": "253 Hoàng Văn Thụ, Quận Phú Nhuận, TP.HCM",
    },
    {
        "_id": ObjectId("000000000000000000000005"),
        "Email": "customer5@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Elsa 5",
        "LastName": "Tran",
        "RoleID": "000000000000000000000003",
        "ProfileImage": "profile_image.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09",
        "Phone": "0765000002",
        "Gender": "Male",
        "Address": "253 Hoàng Văn Thụ, Quận Phú Nhuận, TP.HCM",
    },
    {
        "_id": ObjectId("000000000000000000000006"),
        "Email": "customer6@booking.taxi",
        "Password": "p@ssw0rd",
        "GivenName": "Elsa 6",
        "LastName": "Tran",
        "RoleID": "000000000000000000000003",
        "ProfileImage": "profile_image.png",
        "Deleted": false,
        "CreatedDate": "2023-11-09",
        "Phone": "0765000001",
        "Gender": "Male",
        "Address": "253 Hoàng Văn Thụ, Quận Phú Nhuận, TP.HCM",
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

db.DriverCar.insertMany([
    {
        "_id": ObjectId("000000000000000000000001"),
        "DriverID": "000000000000000000000001",
        "CarTypeID": "000000000000000000000001",
        "Color": "Black",
        "Number": "59L3-12345",
        "Deleted": false
    },
    {
        "_id": ObjectId("000000000000000000000002"),
        "DriverID": "000000000000000000000010",
        "CarTypeID": "000000000000000000000002",
        "Color": "Black",
        "Number": "59L3-67890",
        "Deleted": false
    }
])

db.Booking.insertMany([
    {
        "_id": ObjectId("65557277c135b4bfe5ea0927"),
        "CustomerID": "000000000000000000000003",
        "BookingDate": "2023-11-16",
        "CarTypeID": "000000000000000000000001",
        "CompleteDate": "2998-12-31T17:00:00.000Z",
        "BookingStatusID": "000000000000000000000001",
        "StartLong": "106.6990189",
        "StartLat": "10.7797855",
        "StartAddress": "Nhà thờ Đức Bà, Công xã Paris, Bến Nghé, District 1, Ho Chi Minh City, Vietnam",
        "StartPlaceID": "ChIJUSTY5jcvdTERRVvtbJNZT-g",
        "EndLong": "106.7052906",
        "EndLat": "10.7875434",
        "EndAddress": "Thảo Cầm Viên Sài Gòn, Nguyễn Bỉnh Khiêm, Bến Nghé, District 1, Ho Chi Minh City, Vietnam",
        "EndPlaceID": "ChIJx7wwM0svdTERjuH2a9dkuU0",
        "Distance": "1.2 km",
        "Price": "13200",
        "Duration": "5 mins",
        "MakerAccountID": "000000000000000000000004",
        "Note": "",
        "Deleted": false
    }
])

db.DocumentationImage.insertMany([
    {
        "_id": ObjectId("655d70a77e50c604999be577"),
        "DriverID": "000000000000000000000010",
        "Image": "655d70a77e50c604999be577.jpg"
    },
    {
        "_id": ObjectId("655d70a77e50c604999be578"),
        "DriverID": "000000000000000000000010",
        "Image": "655d70a77e50c604999be578.jpg"
    },
    {
        "_id": ObjectId("655d70a77e50c604999be579"),
        "DriverID": "000000000000000000000001",
        "Image": "655d70a77e50c604999be577.jpg"
    },
    {
        "_id": ObjectId("655d70a77e50c604999be580"),
        "DriverID": "000000000000000000000001",
        "Image": "655d70a77e50c604999be578.jpg"
    }
])
    

