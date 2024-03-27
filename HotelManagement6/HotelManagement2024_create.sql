-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-03-19 21:24:42.859

-- tables
-- Table: Guest
CREATE TABLE Guest (
    ID int  NOT NULL AUTO_INCREMENT,
    FirstName varchar(50)  NOT NULL,
    LastName varchar(50)  NOT NULL,
    GovID varchar(50)  NULL,
    Phone varchar(12)  NULL,
    Email varchar(512)  NULL,
    DateOfBirth date  NULL,
    CONSTRAINT Guest_pk PRIMARY KEY (ID)
);

-- Table: GuestReservationAsc
CREATE TABLE GuestReservationAsc (
    ReservationID int  NOT NULL,
    GuestID int  NOT NULL,
    PrimaryContact bool  NOT NULL DEFAULT 0,
    CONSTRAINT GuestReservationAsc_pk PRIMARY KEY (ReservationID,GuestID)
);

-- Table: Reservation
CREATE TABLE Reservation (
    ID int  NOT NULL AUTO_INCREMENT,
    CheckIn date  NOT NULL,
    CheckOut date  NOT NULL,
    Price numeric(8,2)  NOT NULL,
    CONSTRAINT Reservation_pk PRIMARY KEY (ID)
);

-- Table: ReservationPayments
CREATE TABLE ReservationPayments (
    ReservationID int  NOT NULL,
    Date date  NOT NULL,
    Amount numeric(8,2)  NOT NULL,
    PaymentDetails varchar(50)  NOT NULL,
    ReservationPaymentID int  NOT NULL,
    ID int  NOT NULL AUTO_INCREMENT,
    CONSTRAINT ReservationPayments_pk PRIMARY KEY (ID)
);

-- Table: Room
CREATE TABLE Room (
    ID int  NOT NULL AUTO_INCREMENT,
    Capacity int  NOT NULL,
    Price numeric(8,2)  NOT NULL,
    RoomNumber int  NOT NULL,
    RoomTypeID int  NOT NULL,
    CONSTRAINT Room_pk PRIMARY KEY (ID)
);

-- Table: RoomReservationInt
CREATE TABLE RoomReservationInt (
    RoomID int  NOT NULL,
    ReservationID int  NOT NULL,
    CONSTRAINT RoomReservationInt_pk PRIMARY KEY (RoomID,ReservationID)
);

-- Table: RoomType
CREATE TABLE RoomType (
    RoomTypeID INT  NOT NULL,
    BedSize varchar(20)  NOT NULL,
    NumberOfBeds int  NOT NULL,
    Handicap bool  NOT NULL DEFAULT 0,
    Suite bool  NOT NULL DEFAULT 1,
    CONSTRAINT RoomType_pk PRIMARY KEY (RoomTypeID)
);

-- foreign keys
-- Reference: ReservationPayments_Reservation (table: ReservationPayments)
ALTER TABLE ReservationPayments ADD CONSTRAINT ReservationPayments_Reservation FOREIGN KEY ReservationPayments_Reservation (ReservationID)
    REFERENCES Reservation (ID);

-- Reference: RoomReservationInt_Reservation (table: RoomReservationInt)
ALTER TABLE RoomReservationInt ADD CONSTRAINT RoomReservationInt_Reservation FOREIGN KEY RoomReservationInt_Reservation (ReservationID)
    REFERENCES Reservation (ID);

-- Reference: RoomReservationInt_Room (table: RoomReservationInt)
ALTER TABLE RoomReservationInt ADD CONSTRAINT RoomReservationInt_Room FOREIGN KEY RoomReservationInt_Room (RoomID)
    REFERENCES Room (ID);

-- Reference: Room_RoomType (table: Room)
ALTER TABLE Room ADD CONSTRAINT Room_RoomType FOREIGN KEY Room_RoomType (RoomTypeID)
    REFERENCES RoomType (RoomTypeID);

-- Reference: Table_7_Guest (table: GuestReservationAsc)
ALTER TABLE GuestReservationAsc ADD CONSTRAINT Table_7_Guest FOREIGN KEY Table_7_Guest (GuestID)
    REFERENCES Guest (ID);

-- Reference: Table_7_Reservation (table: GuestReservationAsc)
ALTER TABLE GuestReservationAsc ADD CONSTRAINT Table_7_Reservation FOREIGN KEY Table_7_Reservation (ReservationID)
    REFERENCES Reservation (ID);

-- End of file.

