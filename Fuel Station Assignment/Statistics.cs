using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel_Station_Assignment
{
    class Statistics
    {
	    //Averages
	    private float averageWaitTime; // Average wait time before being fueled.
        private float averageRefuelTime; //Average time taken to refuel a vehicle.
        private float averageBikeRefuelTime; //Average time taken to refuel a Bike.
        private float averageCarRefuelTime; //Average time taken to refuel a Car.
        private float averageVanRefuelTime; //Average time taken to refuel a Van.
        private float averageHGVRefuelTime; //Average time taken to refuel a HGV.

        private float averageTransaction; //Average $ value of transactions.	
        private float averagePetrolTransaction; //Average $ value of petrol transactions.
        private float averageDieselTransaction; //Average $ value of diesel transactions.
        private float averageLpgTransaction; //Average $ value of LPG transactions.

        //Usage Statistics
        private string[] mostUsedFuelType; //An ordered array with position 0 being the most used fuel type.
        private int[] laneUsage; //An ordered array with position 0 being the most used lane.
        private int[] pumpUsage; //An ordered array with position 0 being the most used pump.
    }
}
