using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public sealed class GarageSystemManager
    {
        private readonly Dictionary<string, VehicleListing> r_VehiclesListings = new Dictionary<string, VehicleListing>();

        public LinkedList<string> GetRegistrationNumbersList(VehicleListing.eVehicleStatus? i_StatusToFilterBy = null)
        {
            LinkedList<string> registrationNumbersList = new LinkedList<string>();
            foreach (KeyValuePair<string, VehicleListing> listing in r_VehiclesListings)
            {
                if (i_StatusToFilterBy == null || i_StatusToFilterBy == listing.Value.VehicleStatus)
                {
                    LinkedListNode<string> tempNode = new LinkedListNode<string>(listing.Key);
                    registrationNumbersList.AddLast(tempNode);
                }
            }

            return registrationNumbersList;
        }

        private VehicleListing getListingToChange(string i_RegistrationNumber)
        {
            VehicleListing listingToReturn;
            bool vehicleInCollection = r_VehiclesListings.TryGetValue(i_RegistrationNumber, out listingToReturn);
            if (!vehicleInCollection)
            {
                throw new KeyNotFoundException();
            }
            
            return listingToReturn;
        }

        public void ChangeVehicleState(string i_RegistrationNumber, VehicleListing.eVehicleStatus i_NewStatus)
        {
            VehicleListing listingToChange = getListingToChange(i_RegistrationNumber);
            listingToChange.VehicleStatus = i_NewStatus;
        }

        public bool AddVehicleToDataBase(string i_RegistrationNumber, VehicleInfo i_VehicleInfo, OwnerInfo i_OwnerInfo)
        {
            bool vehicleAlreadyInSystem = true;
            try
            {
                ChangeVehicleState(i_RegistrationNumber, VehicleListing.eVehicleStatus.InRepair);
            }
            catch (KeyNotFoundException)
            {
                r_VehiclesListings.Add(i_RegistrationNumber, new VehicleListing(i_VehicleInfo, i_OwnerInfo));
                vehicleAlreadyInSystem = false;
            }

            return vehicleAlreadyInSystem;
        }


        public void FillTiresOfAVehicleToMax(string i_RegistrationNumber)
        {
            VehicleListing listingToFillTiresIn = getListingToChange(i_RegistrationNumber);
            listingToFillTiresIn.VehicleInfo.FillTiresToMax();
        }

        public void FillFuelTankOfAVehicle(
            string i_RegistrationNumber,
            FuelEnergySource.eFuelType i_FuelType,
            float i_FuelLitersToAdd)
        {
            EnergySource energySource = getListingToChange(i_RegistrationNumber).VehicleInfo.EnergySource;
            FuelSource fuelEnergySource = energySource as FuelEnergySource;
            if (fuelEnergySource == null)
            {
                throw new ArgumentException("The requested vehicle's energy source is not fuel-based!");
            }
            else
            {
                fuelEnergySource.fillEnergy(i_FuelType, i_LitersToFill);
            }
        }

        public void ChargeElectricalVehicle(string i_RegistrationNumber, int i_NumberOfMinutesToCharge)
        {
            EnergySource energySource = getListingToChange(i_RegistrationNumber).VehicleInfo.EnergySource;
            ElectricalSource electricalEnergySource = energySource as FuelEnergySource;
            if (electricalEnergySource == null)
            {
                throw new ArgumentException("The requested vehicle's energy source is not electrical!");
            }
            else
            {
                electricalEnergySource.fillEnergy(i_NumOfMinuteToCharge);
            }
        }
    }
}
