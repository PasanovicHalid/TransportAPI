class Address:
    valid : bool = False
    street = ''
    city = ''
    state = ''
    postal_code = ''
    country = ''
    latitude : float = 0
    longitude : float = 0

    def isValid(self):
        self.valid = self.street != '' and self.city != '' and self.state != '' and self.postal_code != '' and self.country != '' and (self.latitude != 0 or self.latitude != None) and (self.longitude != 0 or self.longitude != None)