from bs4 import BeautifulSoup as bs
import requests
import datetime
import random
import calendar
from transportable_item import TransportableItem
from address import Address
from transportation import Transportation

companyId = 1
driverIds = [2, 3, 4]
minReceivedAmount = 300
maxReceivedAmount = 3000
probability_of_delivered = 0.5
minCostAmount = 0
maxCostAmount = 1000
    
transportable_item_list = [
    TransportableItem('Bike', [0.5, 1.5], [0.5, 1.5], [1.0, 2.0], [0.5, 1.5]),
    TransportableItem('Fridge', [0.5, 1.5], [0.5, 1.5], [1.0, 2.0], [0.5, 1.5]),
    TransportableItem('Washing Machine', [0.5, 1.5], [0.5, 1.5], [1.0, 2.0], [0.5, 1.5]),
    TransportableItem('Motor', [0.5, 1.5], [0.5, 1.5], [1.0, 2.0], [0.5, 1.5]),
    TransportableItem('Furniture', [0.5, 1.5], [0.5, 1.5], [1.0, 2.0], [0.5, 1.5]),
    TransportableItem('Crate', [0.5, 1.5], [0.5, 1.5], [1.0, 2.0], [0.5, 1.5]),
]

def get_soup(url):
    r = requests.get(url)
    soup = bs(r.text, 'html.parser')
    return soup

def get_address(table_with_address):
    address = Address()
    data = table_with_address.find_all('td')

    if(len(data) < 16):
        return address
    
    address.street = data[1].text
    address.city = data[3].text
    address.state = data[5].text
    address.postal_code = data[7].text
    address.country = data[11].text
    address.latitude = data[13].text
    address.longitude = data[15].text
    address.isValid()
    return address

def generate_random_german_address():
    content_of_page = get_soup('https://www.fakexy.com/fake-address-generator-de')
    table_with_address = content_of_page.find('table', {'style': 'margin-top: 35px;'})

    address = get_address(table_with_address)
    return address

def generate_random_transportation():
    startOfMonth = datetime.datetime.now().replace(day=1)
    numberOfDays = calendar.monthrange(startOfMonth.year, startOfMonth.month)[1]

    start = startOfMonth + datetime.timedelta(days=random.randint(0, numberOfDays-1))
    requiredFor = start + datetime.timedelta(days=random.randint(0, 7))
    
    transporting_item = random.choice(transportable_item_list)
    address = generate_random_german_address()
    driverId = random.choice(driverIds)
    received_Amount = random.uniform(minReceivedAmount, maxReceivedAmount)
    received_Currency = "EUR"

    cost_Currency = "EUR"
    isDelivered = random.random() < probability_of_delivered
    if isDelivered:
        cost_Amount = random.uniform(minCostAmount, maxCostAmount)
        start_location = generate_random_german_address()
    else:
        cost_Amount = 0.0
        start_location = Address()

    transportation = Transportation(companyId, start, requiredFor, transporting_item, address, received_Amount, received_Currency, isDelivered, driverId, cost_Amount, cost_Currency, start_location)

    if isDelivered:
        transportation.isValid = address.valid and start_location.valid
    else:
        transportation.isValid = address.valid

    return transportation

def generate_sql_insert_string(transportation : Transportation):
    if transportation.isDelivered:
        return f"""INSERT INTO [dbo].[Transportations]
           ([Start]
           ,[RequiredFor]
           ,[Transporting_Description]
           ,[Transporting_Weight]
           ,[Transporting_Volume_Width]
           ,[Transporting_Volume_Depth]
           ,[Transporting_Volume_Height]
           ,[DriverId]
           ,[CompanyId]
           ,[Deleted]
           ,[Received_Amount]
           ,[Received_Currency]
           ,[Cost_Amount]
           ,[Cost_Currency]
           ,[Destination_City]
           ,[Destination_Country]
           ,[Destination_GpsCoordinate_Latitude]
           ,[Destination_GpsCoordinate_Longitude]
           ,[Destination_PostalCode]
           ,[Destination_State]
           ,[Destination_Street]
           ,[StartLocation_Latitude]
           ,[StartLocation_Longitude])
     VALUES
           ('{transportation.start.strftime('%Y-%m-%d %H:%M:%S.%f')}'
           ,'{transportation.requiredFor.strftime('%Y-%m-%d %H:%M:%S.%f')}'
           ,'{transportation.transporting_description}'
           ,{transportation.transporting_weight}
           ,{transportation.transporting_width}
           ,{transportation.transporting_depth}
           ,{transportation.transporting_height}
           ,{transportation.driverId}
           ,{transportation.companyId}
           ,0
           ,{transportation.received_Amount}
           ,'{transportation.received_Currency}'
           ,{transportation.cost_Amount}
           ,'{transportation.cost_Currency}'
           ,'{transportation.destination_city.replace("'", "")}'
           ,'{transportation.destination_country.replace("'", "")}'
           ,{transportation.destination_latitude}
           ,{transportation.destination_longitude}
           ,'{transportation.destination_postal_code.replace("'", "")}'
           ,'{transportation.destination_state.replace("'", "")}'
           ,'{transportation.destination_street.replace("'", "")}'
           ,{transportation.start_location_latitude}
           ,{transportation.start_location_longitude});"""
    else:
        return f"""INSERT INTO [dbo].[Transportations]
           ([Start]
           ,[RequiredFor]
           ,[Transporting_Description]
           ,[Transporting_Weight]
           ,[Transporting_Volume_Width]
           ,[Transporting_Volume_Depth]
           ,[Transporting_Volume_Height]
           ,[CompanyId]
           ,[Deleted]
           ,[Received_Amount]
           ,[Received_Currency]
           ,[Destination_City]
           ,[Destination_Country]
           ,[Destination_GpsCoordinate_Latitude]
           ,[Destination_GpsCoordinate_Longitude]
           ,[Destination_PostalCode]
           ,[Destination_State]
           ,[Destination_Street])
     VALUES
           ('{transportation.start.strftime('%Y-%m-%d %H:%M:%S.%f')}'
           ,'{transportation.requiredFor.strftime('%Y-%m-%d %H:%M:%S.%f')}'
           ,'{transportation.transporting_description}'
           ,{transportation.transporting_weight}
           ,{transportation.transporting_width}
           ,{transportation.transporting_depth}
           ,{transportation.transporting_height}
           ,{transportation.companyId}
           ,0
           ,{transportation.received_Amount}
           ,'{transportation.received_Currency}'
           ,'{transportation.destination_city.replace("'", "")}'
           ,'{transportation.destination_country.replace("'", "")}'
           ,{transportation.destination_latitude}
           ,{transportation.destination_longitude}
           ,'{transportation.destination_postal_code.replace("'", "")}'
           ,'{transportation.destination_state.replace("'", "")}'
           ,'{transportation.destination_street.replace("'", "")}');"""

if __name__ == "__main__":
    transportations = []
    for i in range(0, 200):
        transportation = generate_random_transportation()
        if transportation.isValid:
            transportations.append(transportation)

        print(f"Generated transportation {i+1} of 200")
    
    with open('transportations.sql', 'w') as f:
        for transportation in transportations:
            f.write(generate_sql_insert_string(transportation) + '\n')
    
    




