using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel_Station_Assignment
{
    class Program
    {
        //Create Lanes
        static Lane lane1 = new Lane(1),
            lane2 = new Lane(2),
            lane3 = new Lane(3);

        //Create Pumps
        static Pump pump1 = new Pump(1, lane1),
            pump2 = new Pump(2, lane1),
            pump3 = new Pump(3, lane1),
            pump4 = new Pump(4, lane2),
            pump5 = new Pump(5, lane2),
            pump6 = new Pump(6, lane2),
            pump7 = new Pump(7, lane3),
            pump8 = new Pump(8, lane3),
            pump9 = new Pump(9 ,lane3);

        //Create Lists
        static List<Pump> pumpList = new List<Pump> { pump1, pump2, pump3, pump4, pump5, pump6, pump7, pump8, pump9 }; //List containing all the pumps in order 1-9.
        public static List<Vehicle> vehiclesWaiting = new List<Vehicle>(); //List of all the vehicles waiting to be fueled.
        public static List<Lane> laneList = new List<Lane> { lane1, lane2, lane3};

        //Create Timers
        static Timer displayUpdate = new Timer(500); //The number of milliseconds between each display update
        static Timer vehicleSpawnRate = new Timer(1500); //The number of miliseconds between new vehicles spawning

        static Random random = new Random(); //Generates a new random number

        static void Main(string[] args)
        {
            //Setup the Vehicle Spawn Timer
            vehicleSpawnRate.Elapsed += new ElapsedEventHandler(VehicleUpdates); //Assigns the CreateVehicle() function to the vehicle spawn timer.
            
            vehicleSpawnRate.Enabled = true; //Turns on and starts the timer.
            
            //Setup the Display Update Timer
            displayUpdate.Elapsed += new ElapsedEventHandler(UpdateDisplay); //Assigns the UpdateDisplay() function to the update display timer.
            displayUpdate.Enabled = true; //Turns on and starts the timer.

            Console.ReadLine();
        }

        static void VehicleUpdates(object source, ElapsedEventArgs e)
        {
            //Randomise the spawn timer.
            vehicleSpawnRate.Interval = random.Next(1500, 2200); //Randomises the interval the timer is running betwen 1.5 - 2.2 seconds.

            if (vehiclesWaiting.Count() > 0) //If there is at least 1 vwhicle waiting to be fueled.
            {
                vehiclesWaiting[0].CheckPumps();
            }
            
            //Check Waiting list length
            if (vehiclesWaiting.Count() >=5) //If there are 5 vehicles in the waiting list already.
            {
                vehiclesWaiting.RemoveAt(vehiclesWaiting.Count-1); //Remove the last vehicle from the list
            }

            //Create new vehicle
            CreateVehicle();
        }

        static void UpdateDisplay(object source, ElapsedEventArgs e)
        {
            Console.Clear(); //Clear the console
            lane1.Display(); //Write Lane 1's pumps status to the console.
            lane2.Display(); //Write Lane 2's pumps status to the console.
            lane3.Display(); //Write Lane 3's pumps status to the console.
            DisplayWaitingList(); //Write the list of vehicles waiting to the console.
        }

        static void DisplayWaitingList()
        {
            Console.WriteLine("\nThere are {0} vehicles waiting.", vehiclesWaiting.Count());
            for (int i = 0; i < vehiclesWaiting.Count; i++)
            {
                Console.WriteLine("{0}) {1}", (i + 1), vehiclesWaiting[i].vehicleType);
            }

            Console.WriteLine("\nPump Values:");
            foreach (Pump pump in pumpList)
            {
                Console.WriteLine("Pump {0}) Petrol: {1}  -  Diesel: {2}  -  LPG: {3}", pump.ID, pump.fuels[1, 0], pump.fuels[1, 1], pump.fuels[1, 2]);
            }
        }

        static void DisplayWaitingListNew()
        {
            Console.SetCursorPosition(Console.CursorLeft, 3);
            Console.WriteLine("\nThere are {0} vehicles waiting.", vehiclesWaiting.Count());

            for (int i = 0; i < vehiclesWaiting.Count; i++)
            {
                Console.SetCursorPosition(Console.CursorLeft, 5 + i);
                Console.WriteLine("{0}) {1}", (i + 1), vehiclesWaiting[i].vehicleType);
            }

            foreach(Pump pump in pumpList)
            {
                Console.WriteLine("Petrol: {0}  -  Diesel: {1}  -  LPG: {2}", pump.fuels[1,0], pump.fuels[1, 2], pump.fuels[2, 2]);
            }
        }

            static void CreateVehicle()
        {
            Vehicle vehicle;
            switch (random.Next(1, 5)) // Between (1 < 5)
            {
                default:
                    vehicle = new Car(); //Create a new Car.
                    break;
                case 1:
                    vehicle = new Bike(); //Create a new Bike.
                    break;
                case 2:
                    vehicle = new Car(); //Create a new Car.
                    break;
                case 3:
                    vehicle = new Van(); //Create a new Van.
                    break;
                case 4:
                    vehicle = new HGV(); //Create a new HGV.
                    break;
            }

            vehiclesWaiting.Add(vehicle); //Add the new vehicle to the waiting list.         
        }

        /*
         static void LaneToPump(Lane lane)
        {
            if (lane.waitingSpace != null) //If there is a vehicle waitig to use the lane
            {
                if(lane.pumps[0].vehicle == null) //If the first pump in the lane is free
                {
                    if (lane.pumps[1].vehicle == null) //If the seconds pump in the lane is free
                    {
                        if (lane.pumps[2].vehicle == null) //If the third pump in the lane is free
                        {
                            lane.pumps[2].vehicle = lane.waitingSpace; //Move the waiting vehicle to the third pump
                            lane.pumps[2].StartRefueling(); //Start the refuelig of the vehicle
                            lane.waitingSpace = null; //Remove the vehicle from the lane waiting list
                        }
                        else
                        {
                            lane.pumps[1].vehicle = lane.waitingSpace; //Move the waiting vehicle to the second pump
                            lane.pumps[1].StartRefueling(); //Start the refuelig of the vehicle
                            lane.waitingSpace = null; //Remove the vehicle from the lane waiting list
                        }
                    }
                    else
                    {
                        lane.pumps[0].vehicle = lane.waitingSpace; //Move the waiting vehicle to the first pump
                        lane.pumps[0].StartRefueling(); //Start the refuelig of the vehicle
                        lane.waitingSpace = null; //Remove the vehicle from the lane waiting list
                    }
                }
            }
        } 
        */

        /*
        static void MoveToLane()
        {
            if (lane1.waitingSpace == null && vehiclesWaiting.Count() > 0) //Check to see if lane 1's waiting space is in use
            {
                lane1.waitingSpace = vehiclesWaiting[0]; //Put the first vehicle in the waiting list to lane 1's waiting space.
                vehiclesWaiting.RemoveAt(0); //Remove the vehicle from the waiting list.
            }
            else if (lane2.waitingSpace == null && vehiclesWaiting.Count() > 0) //Check to see if lane 2's waiting space is in use
            {
                lane2.waitingSpace = vehiclesWaiting[0]; //Put the first vehicle in the waiting list to lane 2's waiting space.
                vehiclesWaiting.RemoveAt(0); //Remove the vehicle from the waiting list.
            }
            else if (lane3.waitingSpace == null && vehiclesWaiting.Count() > 0) //Check to see if lane 3's waiting space is in use
            {
                lane3.waitingSpace = vehiclesWaiting[0]; //Put the first vehicle in the waiting list to lane 3's waiting space.
                vehiclesWaiting.RemoveAt(0); //Remove the vehicle from the waiting list.
            }
        }*/

        static bool CheckForFreePump() //Check to see if a all pumps are in use returns either true or false.
        {
            bool free = false; //The variable to update.

            foreach (Pump pump in pumpList) //Do the check on each pump in the pump list.
            {
                if(pump.vehicle == null) //If a pump is free
                {
                    free = true; //Update te variable/ 
                }
            }

            if(free) //If a pump is free
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}