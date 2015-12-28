﻿using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        private static readonly List<byte> sr_NumOfTires = new List<byte>() { 2, 2, 4, 4, 12 };
        private static readonly List<byte> sr_NumOfFeatures = new List<byte>() { 2, 2, 2, 2, 2 };
        private static readonly List<float> sr_MaxAirPressure = new List<float>() { 32, 32, 29, 29, 34 };

        public enum eVehicleType
        {
            Motorcycle,
            ElectricalMotorCycle,
            Car,
            ElectricalCar,
            Truck
        }

        private static readonly List<string> sr_VehicleTypesDesc = new List<string>(new[]
        {
            "Motorcycle",
            "Electrical MotorCycle",
            "Car",
            "Electrical Car",
            "Truck"
        });

        public static List<string> VehicleTypesDesc
        {
            get
            {
                return sr_VehicleTypesDesc;
            }
        }

        public static VehicleInfo CreateVehicle(eVehicleType i_VehicleType)
        {
            int vehicleTypeIndex = (int)i_VehicleType;
            VehicleInfo vehicleInfo = new VehicleInfo(
                i_VehicleType,
                sr_NumOfTires[vehicleTypeIndex],
                sr_NumOfFeatures[vehicleTypeIndex]);
            addTiresAndUpdateMaxAirPressure(vehicleInfo, sr_MaxAirPressure[vehicleTypeIndex]);

            switch (i_VehicleType)
            {
                case eVehicleType.Motorcycle:
                    createMotorcycle(vehicleInfo);
                    break;
                case eVehicleType.ElectricalMotorCycle:
                    createElectricalMotorCycle(vehicleInfo);
                    break;
                case eVehicleType.Car:
                    createCar(vehicleInfo);
                    break;
                case eVehicleType.ElectricalCar:
                    createElectricalCar(vehicleInfo);
                    break;
                case eVehicleType.Truck:
                    createTruck(vehicleInfo);
                    break;
                default:
                    throw new InvalidEnumArgumentException("i_VehicleType", (int)i_VehicleType, typeof(eVehicleType));
            }

            return vehicleInfo;
        }

        private static void createTruck(VehicleInfo i_VehicleInfo)
        {
            FuelEnergySource.eFuelType k_FuelType = FuelEnergySource.eFuelType.Soler;
            float k_MaxFuelTank = 160;

            i_VehicleInfo.EnergySource = new FuelEnergySource(k_MaxFuelTank, k_FuelType);
            i_VehicleInfo.FeaturesList.Add(new MaxCarryWeightFeature());
            i_VehicleInfo.FeaturesList.Add(new DangerousComponentsCarryFeature());
        }

        private static void createElectricalCar(VehicleInfo i_VehicleInfo)
        {
            float k_MaxBattery = (float)2.8;

            i_VehicleInfo.EnergySource = new ElectricalEnergySource(k_MaxBattery);
            i_VehicleInfo.FeaturesList.Add(new ColorFeature());
            i_VehicleInfo.FeaturesList.Add(new DoorsNumberFeature());
        }

        private static void createCar(VehicleInfo i_VehicleInfo)
        {
            FuelEnergySource.eFuelType k_FuelType = FuelEnergySource.eFuelType.Octan98;
            float k_MaxFuelTank = 42;

            i_VehicleInfo.EnergySource = new FuelEnergySource(k_MaxFuelTank, k_FuelType);
            i_VehicleInfo.FeaturesList.Add(new ColorFeature());
            i_VehicleInfo.FeaturesList.Add(new DoorsNumberFeature());
        }

        private static void createElectricalMotorCycle(VehicleInfo i_VehicleInfo)
        {
            float k_MaxBattery = (float)2.4;

            i_VehicleInfo.EnergySource = new ElectricalEnergySource(k_MaxBattery);
            i_VehicleInfo.FeaturesList.Add(new LicenseTypeFeature());
            i_VehicleInfo.FeaturesList.Add(new EngineCapacityFeature());
        }

        private static void createMotorcycle(VehicleInfo i_VehicleInfo)
        {
            FuelEnergySource.eFuelType k_FuelType = FuelEnergySource.eFuelType.Octan96;
            float k_MaxFuelTank = 6;

            i_VehicleInfo.EnergySource = new FuelEnergySource(k_MaxFuelTank, k_FuelType);
            i_VehicleInfo.FeaturesList.Add(new LicenseTypeFeature());
            i_VehicleInfo.FeaturesList.Add(new EngineCapacityFeature());
        }

        private static void addTiresAndUpdateMaxAirPressure(VehicleInfo i_VehicleInfo, float i_MaxAirPressure)
        {
            byte numOfTires = sr_NumOfTires[(int)i_VehicleInfo.VehicleType];

            for (byte i = 0; i < numOfTires; i++)
            {
                i_VehicleInfo.TiresList.Add(new Tire(i_MaxAirPressure));
            }
        }

        public static eVehicleType ParseVehicleType(string i_VehicleType)
        {
            eVehicleType? parseVehicleTypeResult = null;
            i_VehicleType = i_VehicleType.Trim();

            for (int i = 0; i < sr_VehicleTypesDesc.Count; i++)
            {
                if (string.Equals(sr_VehicleTypesDesc[i], i_VehicleType, StringComparison.CurrentCultureIgnoreCase))
                {
                    parseVehicleTypeResult = (eVehicleType)i;
                }
            }

            if (parseVehicleTypeResult == null)
            {
                throw new FormatException("Parsing vehicle type failed");
            }

            return parseVehicleTypeResult.Value;
        }
    }
}
