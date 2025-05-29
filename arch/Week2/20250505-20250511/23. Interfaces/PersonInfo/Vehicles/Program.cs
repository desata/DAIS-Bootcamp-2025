namespace Vehicles
{
    using System;

    public abstract class Vehicle
    {
        public double FuelQuantity { get; set; }
        public double FuelConsumption { get; set; }
        public double TankCapacity { get; set; }

        public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            if (fuelQuantity > tankCapacity)
            {
                FuelQuantity = 0;
            }
            else
            {
                FuelQuantity = fuelQuantity;
            }

            FuelConsumption = fuelConsumption;
            TankCapacity = tankCapacity;
        }

        public bool Drive(double distance)
        {
            double requiredFuel = distance * FuelConsumption;
            if (requiredFuel <= FuelQuantity)
            {
                FuelQuantity -= requiredFuel;
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void Refuel(double liters)
        {
            if (liters <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return;
            }

            if (FuelQuantity + liters > TankCapacity)
            {
                Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            }
            else
            {
                FuelQuantity += liters;
            }
        }
    }

    public class Car : Vehicle
    {
        private const double AirConditionerFuelConsumption = 0.9;

        public Car(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption + AirConditionerFuelConsumption, tankCapacity)
        {
        }
    }

    public class Truck : Vehicle
    {
        private const double AirConditionerFuelConsumption = 1.6;
        private const double FuelLoss = 0.05;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption + AirConditionerFuelConsumption, tankCapacity)
        {
        }

        public override void Refuel(double liters)
        {
            if (liters <= 0)
            {
                Console.WriteLine("Fuel must be a positive number");
                return;
            }

            if (FuelQuantity + liters > TankCapacity)
            {
                Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            }
            else
            {
                FuelQuantity += liters * (1 - FuelLoss);
            }
        }
    }

    public class Bus : Vehicle
    {
        private const double AirConditionerFuelConsumptionWithPeople = 1.4;

        public bool IsCarryingPeople { get; set; }

        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }

        public void SetCarryingPeople(bool isCarryingPeople)
        {
            IsCarryingPeople = isCarryingPeople;
            if (isCarryingPeople)
            {
                FuelConsumption += AirConditionerFuelConsumptionWithPeople;
            }
            else
            {
                FuelConsumption -= AirConditionerFuelConsumptionWithPeople;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Read input for Car, Truck, and Bus
            string[] carInfo = Console.ReadLine().Split();
            string[] truckInfo = Console.ReadLine().Split();
            string[] busInfo = Console.ReadLine().Split();

            double carFuelQuantity = double.Parse(carInfo[1]);
            double carFuelConsumption = double.Parse(carInfo[2]);
            double carTankCapacity = double.Parse(carInfo[3]);

            double truckFuelQuantity = double.Parse(truckInfo[1]);
            double truckFuelConsumption = double.Parse(truckInfo[2]);
            double truckTankCapacity = double.Parse(truckInfo[3]);

            double busFuelQuantity = double.Parse(busInfo[1]);
            double busFuelConsumption = double.Parse(busInfo[2]);
            double busTankCapacity = double.Parse(busInfo[3]);

            // Instantiate the car, truck, and bus objects
            Vehicle car = new Car(carFuelQuantity, carFuelConsumption, carTankCapacity);
            Vehicle truck = new Truck(truckFuelQuantity, truckFuelConsumption, truckTankCapacity);
            Bus bus = new Bus(busFuelQuantity, busFuelConsumption, busTankCapacity);

            // Read the number of commands
            int n = int.Parse(Console.ReadLine());

            // Execute the commands
            for (int i = 0; i < n; i++)
            {
                string[] commandArgs = Console.ReadLine().Split();

                string command = commandArgs[0];
                string vehicleType = commandArgs[1];

                if (command == "Drive")
                {
                    double distance = double.Parse(commandArgs[2]);

                    if (vehicleType == "Car")
                    {
                        if (car.Drive(distance))
                        {
                            Console.WriteLine($"Car travelled {distance} km");
                        }
                        else
                        {
                            Console.WriteLine("Car needs refueling");
                        }
                    }
                    else if (vehicleType == "Truck")
                    {
                        if (truck.Drive(distance))
                        {
                            Console.WriteLine($"Truck travelled {distance} km");
                        }
                        else
                        {
                            Console.WriteLine("Truck needs refueling");
                        }
                    }
                    else if (vehicleType == "Bus")
                    {
                        if (bus.Drive(distance))
                        {
                            Console.WriteLine($"Bus travelled {distance} km");
                        }
                        else
                        {
                            Console.WriteLine("Bus needs refueling");
                        }
                    }
                }
                else if (command == "DriveEmpty")
                {
                    double distance = double.Parse(commandArgs[2]);

                    if (vehicleType == "Bus")
                    {
                        bus.SetCarryingPeople(false);
                        if (bus.Drive(distance))
                        {
                            Console.WriteLine($"Bus travelled {distance} km");
                        }
                        else
                        {
                            Console.WriteLine("Bus needs refueling");
                        }
                    }
                }
                else if (command == "Refuel")
                {
                    double liters = double.Parse(commandArgs[2]);

                    if (vehicleType == "Car")
                    {
                        car.Refuel(liters);
                    }
                    else if (vehicleType == "Truck")
                    {
                        truck.Refuel(liters);
                    }
                    else if (vehicleType == "Bus")
                    {
                        bus.Refuel(liters);
                    }
                }
            }

            // Output remaining fuel for all vehicles
            Console.WriteLine($"Car: {car.FuelQuantity:F2}");
            Console.WriteLine($"Truck: {truck.FuelQuantity:F2}");
            Console.WriteLine($"Bus: {bus.FuelQuantity:F2}");
        }
    }
}