import datetime
import random
from transportable_item import TransportableItem
from address import Address

class Transportation:
    isValid : bool = False
    isDelivered : bool
    start : datetime.datetime
    requiredFor : datetime.datetime
    transporting_description : str
    transporting_weight : float
    transporting_width : float
    transporting_height : float
    transporting_depth : float
    companyId : int
    received_Amount : float
    received_Currency : str
    destination_street : str
    destination_city : str
    destination_country : str
    destination_postal_code : str
    destination_state : str
    destination_latitude : float
    destination_longitude : float
    driverId : int
    cost_Amount : float
    cost_Currency : str
    start_location_longitude : float
    start_location_latitude : float

    def __init__(self, companyId, start, requiredFor, transporting : TransportableItem, destination_address : Address, received_Amount, received_Currency, isDelivered = False, driverId = 0, cost_Amount = 0.0, cost_Currency = "EUR", start_location: Address = Address()):
        self.companyId = companyId
        self.start = start
        self.requiredFor = requiredFor
        self.assign_transporting_item(transporting)
        self.assign_destination_address(destination_address)
        self.received_Amount = received_Amount
        self.received_Currency = received_Currency

        if isDelivered:
            self.set_as_delivered(start_location, driverId, cost_Amount, cost_Currency)
        else:
            self.isDelivered = isDelivered
            self.driverId = driverId
            self.cost_Amount = cost_Amount
            self.cost_Currency = cost_Currency
            self.start_location_longitude = start_location.longitude
            self.start_location_latitude = start_location.latitude

    def assign_destination_address(self, address : Address):
        self.destination_street = address.street
        self.destination_city = address.city
        self.destination_country = address.country
        self.destination_postal_code = address.postal_code
        self.destination_state = address.state
        self.destination_latitude = address.latitude
        self.destination_longitude = address.longitude

    def assign_transporting_item(self, item : TransportableItem):
        self.transporting_description = item.name
        self.transporting_weight = random.uniform(item.weightRange[0], item.weightRange[1])
        self.transporting_width = random.uniform(item.widthRange[0], item.widthRange[1])
        self.transporting_height = random.uniform(item.heightRange[0], item.heightRange[1])
        self.transporting_depth = random.uniform(item.depthRange[0], item.depthRange[1])

    def set_as_delivered(self, start_location : Address, driverId : int, cost_Amount : float, cost_Currency : str):
        self.isDelivered = True
        self.driverId = driverId
        self.cost_Amount = cost_Amount
        self.cost_Currency = cost_Currency
        self.start_location_longitude = start_location.longitude
        self.start_location_latitude = start_location.latitude

    def print(self):
        print("Transportation:")
        print("    CompanyId: " + str(self.companyId))
        print("    Start: " + str(self.start))
        print("    RequiredFor: " + str(self.requiredFor))
        print("    TransportingDescription: " + self.transporting_description)
        print("    TransportingWeight: " + str(self.transporting_weight))
        print("    TransportingWidth: " + str(self.transporting_width))
        print("    TransportingHeight: " + str(self.transporting_height))
        print("    TransportingDepth: " + str(self.transporting_depth))
        print("    DestinationStreet: " + self.destination_street)
        print("    DestinationCity: " + self.destination_city)
        print("    DestinationCountry: " + self.destination_country)
        print("    DestinationPostalCode: " + self.destination_postal_code)
        print("    DestinationState: " + self.destination_state)
        print("    DestinationLatitude: " + str(self.destination_latitude))
        print("    DestinationLongitude: " + str(self.destination_longitude))
        print("    ReceivedAmount: " + str(self.received_Amount))
        print("    ReceivedCurrency: " + self.received_Currency)
        print("    IsDelivered: " + str(self.isDelivered))
        print("    DriverId: " + str(self.driverId))
        print("    CostAmount: " + str(self.cost_Amount))
        print("    CostCurrency: " + self.cost_Currency)
        print("    StartLocationLongitude: " + str(self.start_location_longitude))
        print("    StartLocationLatitude: " + str(self.start_location_latitude))
        print("")
        