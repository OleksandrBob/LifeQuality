insert into dbo.Patients (Email, Name, Surname, Password, PhoneNumber, RegistrationDate, DoctorId)
values (
        'pat@ukr.net',
        'Steve',
        'Look',
        '111qqqwww',
        '+380444345643',
        '2023-11-11',
        1
       );


insert into dbo.Patients (Email, Name, Surname, Password, PhoneNumber, RegistrationDate, DoctorId)
values (
        'patien@ukr.net',
        'Mike',
        'Stevenson',
        '111qqqwww',
        '+380444321643',
        '2023-11-12',
        1
       );

select * from Patients;