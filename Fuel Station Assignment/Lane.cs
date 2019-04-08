using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuel_Station_Assignment
{
    public class Lane
    {
        public Lane(int num)
        {
            ID = num;
        }

        public int ID; //The ID of the lane.

        public List<Pump> pumps = new List<Pump>(); //List of all the pumps in the lane

        public void Display()
        {
            string[] pumpStates = new string[3];

            if(pumps[0].vehicle != null)
            {
                pumpStates[0] = '(' + pumps[0].vehicle.vehicleType + " - " + pumps[0].vehicle.currentFuel + "/" + pumps[0].vehicle.maxFuel + ')';
            }
            else
            {
                pumpStates[0] = "(Empty)";
            }

            if (pumps[1].vehicle != null)
            {
                pumpStates[1] = '(' + pumps[1].vehicle.vehicleType + " - " + pumps[1].vehicle.currentFuel + "/" + pumps[1].vehicle.maxFuel + ')';
            }
            else
            {
                pumpStates[1] = "(Empty)";
            }

            if (pumps[2].vehicle != null)
            {
                pumpStates[2] = '(' + pumps[2].vehicle.vehicleType + " - " + pumps[2].vehicle.currentFuel + "/" + pumps[2].vehicle.maxFuel + ')';
            }
            else
            {
                pumpStates[2] = "(Empty)";
            }

            while (pumpStates[0].Length < 15)
            {
                pumpStates[0] += "*";
            }
            while (pumpStates[1].Length < 15)
            {
                pumpStates[1] += "*";
            }
            while (pumpStates[2].Length < 15)
            {
                pumpStates[2] += "*";
            }
            Console.WriteLine("Lane {0}  -  ***{1}{2}{3}***", ID, pumpStates[0], pumpStates[1], pumpStates[2]);
        }

        public void DisplayNew(int y)
        {
            string[] pumpStates = new string[3];

            if (pumps[0].vehicle != null)
            {
                pumpStates[0] = '(' + pumps[0].vehicle.vehicleType + " - " + pumps[0].vehicle.currentFuel + "/" + pumps[0].vehicle.maxFuel + ')';
            }
            else
            {
                pumpStates[0] = "(Empty)";
            }

            if (pumps[1].vehicle != null)
            {
                pumpStates[1] = '(' + pumps[1].vehicle.vehicleType + " - " + pumps[1].vehicle.currentFuel + "/" + pumps[1].vehicle.maxFuel + ')';
            }
            else
            {
                pumpStates[1] = "(Empty)";
            }

            if (pumps[2].vehicle != null)
            {
                pumpStates[2] = '(' + pumps[2].vehicle.vehicleType + " - " + pumps[2].vehicle.currentFuel + "/" + pumps[2].vehicle.maxFuel + ')';
            }
            else
            {
                pumpStates[2] = "(Empty)";
            }

            while (pumpStates[0].Length < 15)
            {
                pumpStates[0] += "*";
            }
            while (pumpStates[1].Length < 15)
            {
                pumpStates[1] += "*";
            }
            while (pumpStates[2].Length < 15)
            {
                pumpStates[2] += "*";
            }
            Console.SetCursorPosition(0, y);
            Console.Write("Lane {0}  -  ***{1}{2}{3}***", ID, pumpStates[0], pumpStates[1], pumpStates[2]);
        }
    }
}
