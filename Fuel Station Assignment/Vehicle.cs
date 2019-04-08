using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel_Station_Assignment
{
    public abstract class Vehicle
    {
        //Generic Data
        public string vehicleType; //The type of the vehicle (Bike, Car, Van, HGV).
        public enum fuelTypes { petrol, diesel, LPG}; //The types of fuels the vehicles can use.
        public fuelTypes[] fuels; //The type of fuel the  instaced vehicle can use.

        //Fuel Capacities (litres)
        public int currentFuel; //The current ammount of fuel the vehicle is holding (litres).
        public int maxFuel; //The maxmimum ammount of fuel the vehicle can hold (litres).

        //Random Variables
        protected float patience; //The number of milliseconds the driver is willing to wait before being fueled.

        protected void GenerateData(int _fuel)
        {
            vehicleType = this.GetType().Name; //Gets the type of vehicle from the class name.
            Random random = new Random(); //Random.
            currentFuel = random.Next(1, (int)(maxFuel * 0.25f)); //Generates a randon ammount of fuel between (0 & 1/4 of the vehicles maximum).
        }

        public void CheckPumps()
        {
            //Check each lanes FINAL pump
            foreach (Lane lane in Program.laneList)
            {
                if (lane.pumps[2].vehicle == null) //If pump 3 is free
                {
                    if (lane.pumps[0].vehicle == null && lane.pumps[1].vehicle == null) //If the first and second pumps are free (not blocking access)
                    {
                        //Check to see if the pump has enough of the correct type of fuel
                        for (int i = 0; i < fuels.Count(); i++)
                        {
                            switch((int)fuels[i])
                            {
                                case 0: //Petrol
                                    if (lane.pumps[2].fuels[1,0] > (maxFuel - currentFuel))
                                    {
                                        MoveToPump(lane, 2);
                                        return; //Exit the function as a pump was found for this vehicle.
                                    }
                                    break;

                                case 1: //Diesel
                                    if (lane.pumps[2].fuels[1, 1] > (maxFuel - currentFuel))
                                    {
                                        MoveToPump(lane, 2);
                                        return; //Exit the function as a pump was found for this vehicle.
                                    }
                                    break;

                                case 2: //LPG
                                    if (lane.pumps[2].fuels[1, 2] > (maxFuel - currentFuel))
                                    {
                                        MoveToPump(lane, 2);
                                        return; //Exit the function as a pump was found for this vehicle.
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            //Check each lanes MIDDLE pump
            foreach (Lane lane in Program.laneList)
            {
                if (lane.pumps[1].vehicle == null && lane.pumps[0].vehicle == null) //If pump 2 is free and the first pump is not blocking access
                {                   
                    //Check to see if the pump has enough of the correct type of fuel
                    for (int i = 0; i < fuels.Count(); i++)
                    {
                        switch ((int)fuels[i])
                        {
                            case 0: //Petrol
                                if (lane.pumps[1].fuels[1, 0] > (maxFuel - currentFuel))
                                {
                                    MoveToPump(lane, 1);
                                    return; //Exit the function as a pump was found for this vehicle.
                                }
                                break;

                            case 1: //Diesel
                                if (lane.pumps[1].fuels[1, 1] > (maxFuel - currentFuel))
                                {
                                    MoveToPump(lane, 1);
                                    return; //Exit the function as a pump was found for this vehicle.
                                }
                                break;

                            case 2: //LPG
                                if (lane.pumps[1].fuels[1, 2] > (maxFuel - currentFuel))
                                {
                                    MoveToPump(lane, 1);
                                    return; //Exit the function as a pump was found for this vehicle.
                                }
                                break;
                        }
                    }
                }
            }
            //Check each lanes FIRST pump
            foreach (Lane lane in Program.laneList)
            {
                if (lane.pumps[0].vehicle == null) //If pump 1 is free
                {
                    //Check to see if the pump has enough of the correct type of fuel
                    for (int i = 0; i < fuels.Count(); i++)
                    {
                        switch ((int)fuels[i])
                        {
                            case 0: //Petrol
                                if (lane.pumps[0].fuels[1, 0] > (maxFuel - currentFuel))
                                {
                                    MoveToPump(lane, 0);
                                    return; //Exit the function as a pump was found for this vehicle.
                                }
                                break;

                            case 1: //Diesel
                                if (lane.pumps[0].fuels[1, 1] > (maxFuel - currentFuel))
                                {
                                    MoveToPump(lane, 0);
                                    return; //Exit the function as a pump was found for this vehicle.
                                }
                                break;

                            case 2: //LPG
                                if (lane.pumps[0].fuels[1, 2] > (maxFuel - currentFuel))
                                {
                                    MoveToPump(lane, 0);
                                    return; //Exit the function as a pump was found for this vehicle.
                                }
                                break;
                        }
                    }
                }
            }
        }

        public void MoveToPump(Lane lane, int i)
        {
            lane.pumps[i].vehicle = this; //Set the vehicle at the pump to this vehicle.
            lane.pumps[i].StartRefueling(); //Start refueling the vehicle.
            Program.vehiclesWaiting.RemoveAt(0); //Remove this vehicle from the waiting list.
        }
    }

    class Bike : Vehicle
    {
        public Bike()
        {
            //vehicleType = "Bike";
            maxFuel = 25;
            GenerateData(maxFuel);
            fuels = new fuelTypes[1] { fuelTypes.diesel };
        }
    }

    class Car : Vehicle
    {
        public Car()
        {
            //vehicleType = "Car";           
            maxFuel = 40;
            GenerateData(maxFuel);
            fuels = new fuelTypes[1] { fuelTypes.petrol };
        }
    }

    class Van : Vehicle
    {
        public Van()
        {
            //vehicleType = "Van";
            maxFuel = 80;
            GenerateData(maxFuel);
            fuels = new fuelTypes[2] { fuelTypes.diesel, fuelTypes.LPG };
        }
    }

    class HGV : Vehicle
    {
        public HGV()
        {
            //vehicleType = "HGV";
            maxFuel = 150;
            GenerateData(maxFuel);
            fuels = new fuelTypes[1] { fuelTypes.LPG };
        }
    }
}
