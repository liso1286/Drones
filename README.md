# Drones
Drone functions exam

Drone API consists of 5 services for the management of information related to drones and the medicines they transport. The data is preloaded in a BD In-Memory. 
Queries are made by the SerialNumber attribute of the Drone entity. The SerialNumber of the drones starts at “D1” and is consecutive until “D6”, while the Code 
of the Medications goes from “M1” to “M15”. When the API is raised, the “RegisterDroneBatteryLogJob” task begins to run, which records the state of the drone 
battery level in a file every minute; stored in the main output file as “DroneBattery.log”. Queries can be made using swagger, postman or any other

Below are the examples for making queries for the different services:

<b>/api/DroneQueries/CheckMedicationByDrone</b>

<ul>
<li>
Parameters
{
  "serialNumber": "D1"
}
</li>
</ul>


<b>/api/DroneQueries/CheckAvailablesDronesForLoading</b>

<ul>
<li>
Parameters
{ }
</li>
</ul>


<b>/api/DroneQueries/CheckBatteryLevelForDrone</b>

<ul>
<li>
Parameters
{
  "serialNumber": "D1"
}
</li>
</ul>


<b>/api/DroneCommand/CreateModifyDrone</b>

Note: The same service to modify or create a Drone. BatteryCapacity is expressed in percent must be less than 1.00. 
Model and State are Enum and their schemas are explained at the end of the document.

<ul>
<li>
Parameters
{
  "serialNumber": "D20",
  "model": 3,
  "weightLimit": 34.2,
  "batteryCapacity": 0.60,
  "state": 1
}
</li>
</ul>


<b>/api/DroneCommand/LoadDrone</b>

<i>Note: imagePath can be null</i>

<ul>
<li>Parameters
{
  "droneSerialNumber": "D2",
  "name": "Aspirin",
  "weight": 23,
  "code": "M5",
  "imagePath": "\\home\\itsfoss\\post.jpg"
}
</li>
</ul>


<b>Schemas for the StateLevel and ModelType properties</b>

 <ul>
 <li>
 StateLevel
   {
    Idle = 1,
    Loading = 2,
    Loaded = 3,
    Delivering = 4,
    Delivered = 5,
    Returning = 6
  }
  </li>
  </ul>

  <ul><li>ModelType
   {
    Lightweight = 1,
    Middleweight = 2,
    Cruiserweight = 3,
    Heavyweight = 4
  }
  </li>
  </ul>
