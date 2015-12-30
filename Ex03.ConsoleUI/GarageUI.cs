using System.Text;
using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    using System.Linq;

    public class GarageUI
    {
        private const bool k_OnlyIntegerAllowed = true;
        private static readonly List<string> sr_MenuListDesc = new List<string>(new[]
        {
            "Add vehicle to the garage",
            "List all registration numbers",
            "Change vehicle state",
            "Fill Vehicle Tires",
            "Fill Fuel Tank",
            "Charge Electrical Vehicle",
            "Show vehicle information",
            "Exit"
        });

        private GarageSystemManager m_GarageSystem = new GarageSystemManager();
        private bool m_KeepRunning = true;
        
        private enum eMenuOptions
        {
            AddVehicle,
            PresentRegistrationNumbersList,
            ChangeVehicleState,
            FillTiresOfAVehicleToMax,
            FillFuelTankOfAVehicle,
            ChargeElectricalVehicle,
            ShowVehicleData,
            Exit
        }

        private enum eInputKeyConstraints
        {
            New,
            Existing,
        }

        public void StartGarageManagementProgram()
        {
            while (m_KeepRunning)
            {
                showMenuAndHandleRequests();
            }
        }

        private void showMenuAndHandleRequests()
        {
                Console.Clear();
                Console.WriteLine("Welcome to The Garage Mangament System");
                int userChoice = ConsoleUtilities.ChooseEnumValue(typeof(eMenuOptions), sr_MenuListDesc);
                handleUserChoice((eMenuOptions)userChoice);
        }

        private void handleUserChoice(eMenuOptions i_UserChoice)
        {
            switch (i_UserChoice)
            {
                case eMenuOptions.AddVehicle:
                    this.addVehicleToSystem();
                    break;
                case eMenuOptions.PresentRegistrationNumbersList:
                    this.presentRegistrationNumbersList();
                    break;
                case eMenuOptions.ChangeVehicleState:
                    this.changeVehicleState();
                    break;
                case eMenuOptions.FillTiresOfAVehicleToMax:
                    this.fillTiresOfAVehicleToMax();
                    break;
                case eMenuOptions.FillFuelTankOfAVehicle:
                    this.fillFuelTankOfAVehicle();
                    break;
                case eMenuOptions.ChargeElectricalVehicle:
                    this.chargeElectricalVehicle();
                    break;
                case eMenuOptions.ShowVehicleData:
                    this.showVehicleData();
                    break;
                case eMenuOptions.Exit:
                    m_KeepRunning = false;
                    break;
                default:
                    throw new ValueOutOfRangeException("Enum value does not Exists", (float)i_UserChoice, 0, (float)GarageSystemManager.GetHighestValueForEnum<eMenuOptions>());
            }
        }

        private void addVehicleToSystem()
        {
            string newRegistrationNumber;
            OwnerInfo newOwnerInfo;
            VehicleInfo newVehicleInfo;

            try
            {
                newVehicleInfo = getNewVehicleInfo();
                newRegistrationNumber = this.getRegistrationNumber(eInputKeyConstraints.New);
                newOwnerInfo = getNewOwnerInfo();
                updateVehicleExtraInfo(newVehicleInfo);
                this.m_GarageSystem.AddVehicleToDataBase(newRegistrationNumber, newVehicleInfo, newOwnerInfo);
            }
            catch (Exception exception)
            {  
                ConsoleUtilities.PromptMessage("Failed to add Vehicle to system. Info: " + exception.Message);
            }
        }
        
        private OwnerInfo getNewOwnerInfo()
        {
            OwnerInfo newOwnerInfo;
            string ownerName;
            string ownerPhoneNumber;

            ownerName = ConsoleUtilities.GetInputString("Please Enter Owner Name");
            ownerPhoneNumber = ConsoleUtilities.GetInputString("Please Enter Phone Number");
            newOwnerInfo = new OwnerInfo(ownerName, ownerPhoneNumber);

            return newOwnerInfo;
        }

        private VehicleInfo getNewVehicleInfo()
        {
            int userChoice = ConsoleUtilities.ChooseEnumValue(typeof(VehicleCreator.eVehicleType), VehicleCreator.VehicleTypesDesc);
            
            return VehicleCreator.CreateVehicle((VehicleCreator.eVehicleType)userChoice);
        }

        private string getRegistrationNumber()
        {
            string rawRegistartionNumber =
                ConsoleUtilities.GetInputString("Please enter registration number (Case insensitive)");
            string formattedRegNum = rawRegistartionNumber.ToUpper();
            formattedRegNum = formattedRegNum.Replace(" ", string.Empty);
            return formattedRegNum;
        }

        private string getRegistrationNumber(eInputKeyConstraints i_InputKeyConstraints)
        {
            bool inputValid = false;
            string registrationNumber;

            do
            {
                registrationNumber = this.getRegistrationNumber();
                switch (i_InputKeyConstraints)
                {
                    case eInputKeyConstraints.New:
                        inputValid = !this.m_GarageSystem.IsRegistartionNumberExists(registrationNumber);
                        if (!inputValid)
                        {
                            ConsoleUtilities.ShowBadInputMessage("Registration number already exists");
                        }

                        break;
                    case eInputKeyConstraints.Existing:
                        inputValid = this.m_GarageSystem.IsRegistartionNumberExists(registrationNumber);
                        if (!inputValid)
                        {
                            ConsoleUtilities.ShowBadInputMessage("Registration number Does not exists");
                        }

                        break;
                    default:
                        throw new ValueOutOfRangeException("Enum value does not Exists", (float)i_InputKeyConstraints, 0, (float)GarageSystemManager.GetHighestValueForEnum<eInputKeyConstraints>());
                }
            }
            while (!inputValid);
            
            return registrationNumber;
        }

        private void updateVehicleExtraInfo(VehicleInfo i_NewVehicleInfo)
        {
                i_NewVehicleInfo.ModelName = ConsoleUtilities.GetInputString("Enter Model Name");
                getAndUpdateTiresInfo(i_NewVehicleInfo);
                getAndUpdateEnergySourceInfo(i_NewVehicleInfo);
                getAndUpdateFeaturesInfo(i_NewVehicleInfo);
        }

        private void getAndUpdateEnergySourceInfo(VehicleInfo i_NewVehicleInfo)
        {
            FuelEnergySource fuelEnergySource = i_NewVehicleInfo.EnergySource as FuelEnergySource;
            if (fuelEnergySource != null)
            {
                getAndUpdateFuelEnergyData(fuelEnergySource);
            }
            else
            {
                ElectricalEnergySource electricEnergySource = i_NewVehicleInfo.EnergySource as ElectricalEnergySource;
                if (electricEnergySource == null)
                {
                    throw new InvalidOperationException("Vehicle's Energy source down casting failed for all child classes");
                }

                getAndUpdateElectricalEnergyData(electricEnergySource);
            }
        }

        private void getAndUpdateElectricalEnergyData(ElectricalEnergySource i_ElectricEnergySource)
        {
            string userMessage = string.Format(
                "Please Enter Remainding Battery Hours (Max value: {0})",
                i_ElectricEnergySource.MaxBatteryHours);
            float batterHoursLeft =
                (float)
                ConsoleUtilities.GetPosNumFromUser(userMessage, !k_OnlyIntegerAllowed, (decimal)i_ElectricEnergySource.MaxBatteryHours);
            i_ElectricEnergySource.BatteryHoursLeft = batterHoursLeft;
        }

        private void getAndUpdateFuelEnergyData(FuelEnergySource i_FuelEnergySource)
        {
            string userMessage = string.Format(
                "Please Enter Remainding Fuel Liters(Max value: {0})",
                i_FuelEnergySource.MaxFuelLitersAmount);
            float fuelLiterAmount =
                (float)
                ConsoleUtilities.GetPosNumFromUser(userMessage, !k_OnlyIntegerAllowed, (decimal)i_FuelEnergySource.MaxFuelLitersAmount);
            i_FuelEnergySource.CurrFuelLitersAmount = fuelLiterAmount;
        }

        private void getAndUpdateTiresInfo(VehicleInfo i_NewVehicleInfo)
        {
            int tireIndex = 0;
            string userMessage;

            foreach (Tire tire in i_NewVehicleInfo.TiresList)
            {
                tireIndex++;
                userMessage = string.Format("Please Enter tire number {0}'s Manufactor Name", tireIndex);
                tire.ManufacturerName = ConsoleUtilities.GetInputString(userMessage);
                userMessage = string.Format("Please Enter tire number {0}'s current Air Pressure(Max value: {1})", tireIndex, tire.MaxAirPressure);
                tire.CurrAirPressure = (float)ConsoleUtilities.GetPosNumFromUser(userMessage, !k_OnlyIntegerAllowed, (decimal)tire.MaxAirPressure);
            }
        }

        private void getAndUpdateFeaturesInfo(VehicleInfo i_NewVehicleInfo)
        {
            foreach (Feature feature in i_NewVehicleInfo.FeaturesList)
            {
                string inputFeatureValue;
                bool inputFeatureValid = false;
                string userMessage = string.Format("Please enter {0}: ", feature.Description);

                do
                {
                    inputFeatureValue = ConsoleUtilities.GetInputString(userMessage);
                    inputFeatureValid = feature.IsValid(inputFeatureValue);

                    if (!inputFeatureValid)
                    {
                        ConsoleUtilities.ShowBadInputMessage("Property did not entered as requested");
                    }
                }
                while (!inputFeatureValid);
                feature.SetValue(inputFeatureValue);
            }
        }

        private void presentRegistrationNumbersList()
        {
            try
            {
                VehicleListing.eVehicleStatus? statusToFilterBy;
                bool filterListing = ConsoleUtilities.AskUserBooleanQuestion(
                    "Would you like to filter Registration Number list by current status?");

                if (filterListing)
                {
                    statusToFilterBy = this.getVehicleStatus();
                }
                else
                {
                    statusToFilterBy = null;
                }

                this.presentRegistrationNumbersList(statusToFilterBy);
            }
            catch (Exception exception)
            {
                ConsoleUtilities.PromptMessage("Failed to show Registration Number List. Info: " + exception.Message);
            }
        }

        private VehicleListing.eVehicleStatus getVehicleStatus()
        {
            int userChoice = ConsoleUtilities.ChooseEnumValue(typeof(VehicleListing.eVehicleStatus));

            return (VehicleListing.eVehicleStatus)userChoice;
        }

        private FuelEnergySource.eFuelType getFuelType()
        {
            int userChoice = ConsoleUtilities.ChooseEnumValue(typeof(FuelEnergySource.eFuelType));

            return (FuelEnergySource.eFuelType)userChoice;
        }

        private void presentRegistrationNumbersList(VehicleListing.eVehicleStatus? i_VehicleStatusToFilterBy)
        {
            StringBuilder messageToUser = new StringBuilder("Registration number Listing : " + Environment.NewLine);
            LinkedList<string> registrationNumberList =
                this.m_GarageSystem.GetRegistrationNumbersList(i_VehicleStatusToFilterBy);
           
            if(!registrationNumberList.Any())
            {
                messageToUser.Append("Listing is empty!");
            }
            else
            {
                foreach (string registrationNumber in registrationNumberList)
                {
                    messageToUser.Append(registrationNumber + Environment.NewLine);
                }
            }
            
            ConsoleUtilities.PromptMessage(messageToUser.ToString());
        }

        private void changeVehicleState()
        {
            try
            {
                string registrationNumber = this.getRegistrationNumber(eInputKeyConstraints.Existing);
                VehicleListing.eVehicleStatus newStatus = this.getVehicleStatus();
                this.m_GarageSystem.ChangeVehicleState(registrationNumber, newStatus);
            }
            catch (Exception exception)
            {
                ConsoleUtilities.PromptMessage("Failed to change vehicle state. Info: " + exception.Message);   
            }
        }

        private void fillTiresOfAVehicleToMax()
        {
            try
            {
                string registrationNumber = this.getRegistrationNumber(eInputKeyConstraints.Existing);
                this.m_GarageSystem.FillTiresOfAVehicleToMax(registrationNumber);
            }
            catch (Exception exception)
            {
                ConsoleUtilities.PromptMessage("Failed to fill tires. Info: " + exception.Message);   
            }
        }

        private void fillFuelTankOfAVehicle()
        {
            try
            {
                string registrationNumber = this.getRegistrationNumber(eInputKeyConstraints.Existing);
                VehicleListing vehicleListing = this.m_GarageSystem.GetListing(registrationNumber);
                FuelEnergySource fuelEnergySource = vehicleListing.VehicleInfo.EnergySource as FuelEnergySource;
                FuelEnergySource.eFuelType fuelTypeToFill;
                bool fuelTypeIsValid;

                if (fuelEnergySource == null)
                {
                    throw new ArgumentException("The requested vehicle's energy source is not fuel-based!");
                }

                do
                {
                    fuelTypeToFill = this.getFuelType();
                    fuelTypeIsValid = fuelTypeToFill == fuelEnergySource.FuelType;
                }
                while (!fuelTypeIsValid);

                string userMessage = string.Format(
                    "Please Enter the Amount of Litters to Fill (Max value: {0})",
                    fuelEnergySource.MaxFuelLitersAmountToFill);
                float fuelAmountToFill = (float)ConsoleUtilities.GetPosNumFromUser(userMessage, !k_OnlyIntegerAllowed, (decimal)fuelEnergySource.MaxFuelLitersAmountToFill);
                this.m_GarageSystem.FillFuelTankOfAVehicle(registrationNumber, fuelTypeToFill, fuelAmountToFill);
            }
            catch (Exception exception)
            {
                ConsoleUtilities.PromptMessage("Failed to fill fuel tank. Info: " + exception.Message);   
            }
        }

        private void chargeElectricalVehicle()
        {
            try
            {
                string registrationNumber = this.getRegistrationNumber(eInputKeyConstraints.Existing);
                VehicleListing vehicleListing = this.m_GarageSystem.GetListing(registrationNumber);
                ElectricalEnergySource electricalEnergySource = vehicleListing.VehicleInfo.EnergySource as ElectricalEnergySource;
                if (electricalEnergySource == null)
                {
                    throw new ArgumentException("The requested vehicle's energy source is not electricity-based!");
                }

                string userMessage = string.Format(
                    "Please Enter Number Of Minutes To Charge (Max value: {0})",
                    electricalEnergySource.MaxBatteryHoursToFill);
                int numOfMinutesToCharge = (int)ConsoleUtilities.GetPosNumFromUser(userMessage, k_OnlyIntegerAllowed, (decimal)electricalEnergySource.MaxBatteryHoursToFill);
                this.m_GarageSystem.ChargeElectricalVehicle(registrationNumber, numOfMinutesToCharge);
            }
            catch (Exception exception)
            {
                ConsoleUtilities.PromptMessage("Failed to Charge battery. Info: " + exception.Message);   
            }
        }

        private void showVehicleData()
        {
            try
            {
                string registrationNumber = this.getRegistrationNumber(eInputKeyConstraints.Existing);
                VehicleListing vehicleListing = this.m_GarageSystem.GetListing(registrationNumber);
                string messageToUser = string.Format(
@"
Vehicle Data:
Registration Number: {0}
{1}",
                registrationNumber,
                vehicleListing);
                ConsoleUtilities.PromptMessage(messageToUser);
            }
            catch (Exception exception)
            {
                ConsoleUtilities.PromptMessage("Failed to show vehicle data. Info: " + exception.Message);   
            }
        }
    }
}
