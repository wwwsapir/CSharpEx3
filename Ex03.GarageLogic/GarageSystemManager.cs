using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public sealed class GarageSystemManager
    {
        private readonly Dictionary<string, VehicleListing> r_VehiclesListings = new Dictionary<string, VehicleListing>();

        public static TEnum GetHighestValueForEnum<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Max();
        }

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

        //Checks if a car with the given registration number exists in the dictionary
        public bool IsRegistartionNumberExists(string i_RegistrationNumber)
        {
            return this.r_VehiclesListings.ContainsKey(i_RegistrationNumber);
        }

        //Returns a listing from the dictionary with the registration number as a key
        public VehicleListing GetListing(string i_RegistrationNumber)
        {
            VehicleListing listingToReturn;
            bool vehicleInCollection = r_VehiclesListings.TryGetValue(i_RegistrationNumber, out listingToReturn);
            if (!vehicleInCollection)
            {
                throw new KeyNotFoundException("There is no Vehicle in the garage with the given Registration number");
            }
            
            return listingToReturn;
        }

        //Changes the vehicle status (In repaire, repaired, paid)
        public void ChangeVehicleState(string i_RegistrationNumber, VehicleListing.eVehicleStatus i_NewStatus)
        {
            VehicleListing listingToChange = this.GetListing(i_RegistrationNumber);
            listingToChange.VehicleStatus = i_NewStatus;
        }

        public void AddVehicleToDataBase(string i_RegistrationNumber, VehicleInfo i_VehicleInfo, OwnerInfo i_OwnerInfo)
        {
            if (this.r_VehiclesListings.ContainsKey(i_RegistrationNumber))
            {
                throw new ArgumentException("Key already exists");
            }
            
            r_VehiclesListings.Add(i_RegistrationNumber, new VehicleListing(i_VehicleInfo, i_OwnerInfo));
        }

        public void FillTiresOfAVehicleToMax(string i_RegistrationNumber)
        {
            VehicleListing listingToFillTiresIn = this.GetListing(i_RegistrationNumber);
            listingToFillTiresIn.VehicleInfo.FillTiresToMax();
        }

        public void FillFuelTankOfAVehicle(
            string i_RegistrationNumber,
            FuelEnergySource.eFuelType i_FuelType,
            float i_FuelLitersToAdd)
        {
            EnergySource energySource = this.GetListing(i_RegistrationNumber).VehicleInfo.EnergySource;
            FuelEnergySource fuelEnergySource = energySource as FuelEnergySource;
            if (fuelEnergySource == null)
            {
                throw new ArgumentException("The requested vehicle's energy source is not fuel-based!");
            }
            
            fuelEnergySource.FillFuelTank(i_FuelType, i_FuelLitersToAdd);
        }

        public void ChargeElectricalVehicle(string i_RegistrationNumber, int i_NumberOfMinutesToCharge)
        {
            EnergySource energySource = this.GetListing(i_RegistrationNumber).VehicleInfo.EnergySource;
            ElectricalEnergySource electricalEnergySource = energySource as ElectricalEnergySource;
            if (electricalEnergySource == null)
            {
                throw new ArgumentException("The requested vehicle's energy source is not electrical!");
            }
            else
            {
                electricalEnergySource.FillBattery(i_NumberOfMinutesToCharge);
            }
        }       
    }
}
