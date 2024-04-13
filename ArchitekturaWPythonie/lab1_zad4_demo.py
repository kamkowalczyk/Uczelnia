import logging

logging.basicConfig(level=logging.INFO)

class EmailValidator:
    def set(self, instance, value):
        if '@wsei.edu.pl' in value:
            instance.__dict__[self.name] = value
        else:
            raise ValueError("Invalid email address")
        
class Student:
    email = EmailValidator()
    def __init__(self, email, imie, nazwizko):
        self.email = email
        self.imie = imie
        self.nazwizko = nazwizko

Student("jan.kowalczki@wsei.edu.pl", "Jan", "Kowalski")
