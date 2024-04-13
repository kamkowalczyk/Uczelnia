import logging

logging.basicConfig(level=logging.INFO)

class Descriptor:
    def __init__(self, name=None):
        self.name = name

    def __get__(self, instance, owner):
        if instance is not None:
            value = instance.__dict__.get(self.name, "missing value")
            logging(f"{self.name}: {value}")
            return value
        return self

    def __set__(self, instance, value):
        logging(f"Saved: {self.name}: {value}")
        instance.__dict__[self.name] = value

class Uzytkownik:
    imie = Descriptor('imie')
    wiek = Descriptor('wiek')

    def __init__(self, imie, wiek):
        self.imie = imie
        self.wiek = wiek