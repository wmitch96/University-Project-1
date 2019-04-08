using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Fuel_Station_Assignment
{
    public class Pump
    {
        public Pump(int num, Lane l)
        {
            lane = l; //The lane the pump is assigned to.
            ID = num; //The Pumps ID / Number
            lane.pumps.Add(this); //Adds the pump to the lanes list.

            //Petrol
            fuels[0, 0] = 0; //Petrol
            fuels[1, 0] = 250; //Petrol Capacity

            //Diesel
            fuels[0, 1] = 1; //Diesel
            fuels[1, 1] = 250; //Diesel Capacity

            //LPG
            fuels[0, 2] = 2; //LPG
            fuels[1, 2] = 250; //LPG Capacity
        }

        public int ID; //The Pumps ID.

        Lane lane; //The lane the pump is aassigned to

        public Vehicle vehicle;

        static float refuelRate = 1; //The number of seconds it takes to add 1 litre of fuel to the vehicle

        Timer refuelTimer = new Timer(refuelRate * 1000);

        public int[] fuelTypes = new int[] { 0, 1, 2 }; //The types of fuels the vehicles can use.
        public int[,] fuels = new int[3,3]; //The types of fuels the pump can dispence and ammount of each fuel avaliable.
       

        //Fuel Capacities (litres).
        protected float petrolCapacity = 250; //The maxmimum ammount of fuel the pump can hold (litres).
        protected float dieselCapacity = 250; //The maxmimum ammount of fuel the pump can hold (litres).
        protected float lpgCapacity = 250; //The maxmimum ammount of fuel the pump can hold (litres).

        //Current Fuel Remaining (litres).
        protected float currentPetrol; //The remaining ammount of petrol the pump is holding (litres).
        protected float currentDiesel; //The remaining ammount of diesel the pump is holding (litres).
        protected float currentLpg; //The remaining ammount of LPG the pump is holding (litres).

        //Track daily ammount of fuel dispenced.
        protected float petrolDispenced; //The ammount of petrol this pump has dispenced on the current day.
        protected float dieselDispenced; //The ammount of diesel this pump has dispenced on the current day.
        protected float lpgDispenced; //The ammount of LPG this pump has dispenced on the current day.

        //Track lifetime total ammount of fuel dispenced.
        protected float petrolDispencedTotal; //The total ammount of petrol this pump has dispenced.
        protected float dieselDispencedTotal; //The total ammount of diesel this pump has dispenced.
        protected float lpgDispencedTotal; //The total ammount of LPG this pump has dispenced.

        //Track the ammount of money the pump has taken in.
        protected float totalIncome; //The total ammount of money this pump has made.
        protected float biggestTransaction; //The largest single monitary transaction this pump has recieved. 
        protected string mostProfitableDay; //The date this pump made the most money. (Date/Time)

        public void StartRefueling()
        {
            refuelTimer.Enabled = true;
            refuelTimer.Elapsed += new ElapsedEventHandler(RefuelVehicle); //Assigns the RefuelVehicle() function to the refuel timer.
            refuelTimer.Start();           
        }

        private void RefuelVehicle(object source, ElapsedEventArgs e)
        {
            if (vehicle == null) //If there is no vehicle at the pump
            {                
                if(refuelTimer.Enabled == true) //If the refuel timer is turned on
                {
                    refuelTimer.Enabled = false; //Turn the refuel timer off
                }
                return; //Exit
            }
            else //If there is a vehicle at the pump
            {
                if (vehicle.currentFuel < vehicle.maxFuel) //If the vehicle needs more fuel
                {
                    Random fuelRand = new Random();
                    int fuel = fuelRand.Next(0, (vehicle.fuels.Count()));

                    switch ((int)vehicle.fuels[fuel]) //Cast the fuels enum to an int
                    {                            
                        //Petrol
                        case 0:
                            if (fuels[1,0] >= (vehicle.maxFuel - vehicle.currentFuel)) //If there is enough fuel to fully refule the vehicle
                            {
                                vehicle.currentFuel++; //Add Fuel
                                fuels[1, 0]--; //Remove fuel from the pump
                            }
                            else //If the vehicle cannot get enough fuel from the pump rejoin the queue and try another pump.
                            {
                                Program.vehiclesWaiting.Add(vehicle);
                                vehicle = null;
                                if (refuelTimer.Enabled == true) //If the refuel timer is still on
                                {
                                    refuelTimer.Enabled = false; //Turn the refuel timer off
                                }
                            }
                            break;

                        //Diesel
                        case 1:
                            if (fuels[1, 1] >= (vehicle.maxFuel - vehicle.currentFuel)) //If there is enough fuel to fully refule the vehicle
                            {
                                vehicle.currentFuel++; //Add Fuel
                                fuels[1, 1]--; //Remove fuel from the pump
                            }
                            else //If the vehicle cannot get enough fuel from the pump rejoin the queue and try another pump.
                            {
                                Program.vehiclesWaiting.Add(vehicle);
                                vehicle = null;
                                if (refuelTimer.Enabled == true) //If the refuel timer is still on
                                {
                                    refuelTimer.Enabled = false; //Turn the refuel timer off
                                }
                            }
                            break;

                            //LPG
                        case 2:
                            if (fuels[1, 2] >= (vehicle.maxFuel - vehicle.currentFuel)) //If there is enough fuel to fully refule the vehicle
                            {
                                vehicle.currentFuel++; //Add Fuel
                                fuels[1, 2]--; //Remove fuel from the pump
                            }
                            else //If the vehicle cannot get enough fuel from the pump rejoin the queue and try another pump.
                            {
                                Program.vehiclesWaiting.Add(vehicle);
                                vehicle = null;
                                if (refuelTimer.Enabled == true) //If the refuel timer is still on
                                {
                                    refuelTimer.Enabled = false; //Turn the refuel timer off
                                }
                            }
                            break;
                    }
                }
                else //If the vehicle does not need more fuel
                {
                    vehicle = null; //Remove the vehicle from the pump
                    if (refuelTimer.Enabled == true) //If the refuel timer is still on
                    {
                        refuelTimer.Enabled = false; //Turn the refuel timer off
                    }
                }
            }
        }
    }
}