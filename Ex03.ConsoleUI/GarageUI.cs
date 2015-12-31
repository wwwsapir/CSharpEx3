using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public sealed class GarageUI
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
            NoConstraints
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
            string inputRegistrationNumber;
            OwnerInfo newOwnerInfo;
            VehicleInfo newVehicleInfo;

            try
            {
                inputRegistrationNumber = this.getRegistrationNumber(eInputKeyConstraints.NoConstraints);
                if (this.m_GarageSystem.IsRegistartionNumberExists(inputRegistrationNumber))
                {
                    // If vehicle already is in the system then updating only basic info regarding tires and energy source
                    VehicleListing existingVehicleListing = this.m_GarageSystem.GetListing(inputRegistrationNumber);
                    existingVehicleListing.VehicleStatus = VehicleListing.eVehicleStatus.InRepair;
                    Console.WriteLine("Vehicle already exists in system. Status changed to 'InRepaire'.");
                    getAndUpdateTiresInfo(existingVehicleListing.VehicleInfo);
                    getAndUpdateEnergySourceInfo(existingVehicleListing.VehicleInfo);
                }
                else
                {
                    // Creating new vehicle from scratch
                    newVehicleInfo = getNewVehicleInfo();
                    newOwnerInfo = getNewOwnerInfo();
                    updateVehicleExtraInfo(newVehicleInfo);
                    m_GarageSystem.AddVehicleToDataBase(inputRegistrationNumber, newVehicleInfo, newOwnerInfo);
                    ConsoleUtilities.PromptMessage(string.Format("vehicle ({0}) added successfully!", inputRegistrationNumber));
                }
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

        // Instantiating new VehicleInfo object, filling in the most basic known data using vehicleCreator class
        private VehicleInfo getNewVehicleInfo()
        {
            int userChoice = ConsoleUtilities.ChooseEnumValue(typeof(VehicleCreator.eVehicleType), VehicleCreator.VehicleTypesDesc);
            
            return VehicleCreator.CreateVehicle((VehicleCreator.eVehicleType)userChoice);
        }

        // Getting reg. number, then  formatted as upper case and without spaces
        private string getRegistrationNumber()
        {
            string rawRegistartionNumber =
                ConsoleUtilities.GetInputString("Please enter registration number (Case insensitive)");
            string formattedRegNum = rawRegistartionNumber.ToUpper();
            formattedRegNum = formattedRegNum.Replace(" ", string.Empty);
            return formattedRegNum;
        }

        // getting registration number with certain constraints regarding the number
        private string getRegistrationNumber(eInputKeyConstraints i_InputKeyConstraints)
        {
            bool inputValid;
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
                    case eInputKeyConstraints.NoConstraints:
                        inputValid = true;
                        break;
                    default:
                        throw new ValueOutOfRangeException("Enum value does not Exists", (float)i_InputKeyConstraints, 0, (float)GarageSystemManager.GetHighestValueForEnum<eInputKeyConstraints>());
                }
            }
            while (!inputValid);
            
            return registrationNumber;
        }

        // filling Vehicle info from user
        private void updateVehicleExtraInfo(VehicleInfo i_NewVehicleInfo)
        {
                i_NewVehicleInfo.ModelName = ConsoleUtilities.GetInputString("Enter Model Name");
                getAndUpdateTiresInfo(i_NewVehicleInfo);
                getAndUpdateEnergySourceInfo(i_NewVehicleInfo);
                getAndUpdateFeaturesInfo(i_NewVehicleInfo);
        }

        // Checking exact type of EnergySource and acts accordingly
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
            float batterHoursLeft;
            bool inputIsValid = false;

            while (!inputIsValid)
            {
                try
                {
                    string userMessage = string.Format(
                        "Please Enter Remaining Battery Hours (Max value: {0})",
                        i_ElectricEnergySource.MaxBatteryHours);
                    batterHoursLeft = (float)ConsoleUtilities.GetNonNegativeNumFromUser(userMessage, !k_OnlyIntegerAllowed);
                    i_ElectricEnergySource.BatteryHoursLeft = batterHoursLeft;
                    inputIsValid = true;
                }
                catch (Exception exception)
                {
                    ConsoleUtilities.ShowBadInputMessage(exception.Message);
                }
            }
        }

        private void getAndUpdateFuelEnergyData(FuelEnergySource i_FuelEnergySource)
        {
            float fuelLiterAmount;
            bool inputIsValid = false;
            while (!inputIsValid)
            {
                try
                {
                    string userMessage = string.Format(
                        "Please Enter Remainding Fuel Liters(Max value: {0})",
                        i_FuelEnergySource.MaxFuelLitersAmount);
                    fuelLiterAmount =
                        (float)ConsoleUtilities.GetNonNegativeNumFromUser(userMessage, !k_OnlyIntegerAllowed);
                    i_FuelEnergySource.CurrFuelLitersAmount = fuelLiterAmount;
                    inputIsValid = true;
                }
                catch (Exception exception)
                {
                    ConsoleUtilities.ShowBadInputMessage(exception.Message);
                }
            }
        }

        private void getAndUpdateTiresInfo(VehicleInfo i_NewVehicleInfo)
        {
            int tireIndex = 0;
            string userMessage;

            // Here we assume that all tires have been created already
            foreach (Tire tire in i_NewVehicleInfo.TiresList)
            {
                tireIndex++;
                bool inputIsValid = false;

                while (!inputIsValid)
                {
                    try
                    {
                        userMessage = string.Format("Please Enter tire number {0}'s Manufactor Name", tireIndex);
                        tire.ManufacturerName = ConsoleUtilities.GetInputString(userMessage);
                        userMessage = string.Format("Please Enter tire number {0}'s current Air Pressure(Max value: {1})", tireIndex, tire.MaxAirPressure);
                        tire.CurrAirPressure = (float)ConsoleUtilities.GetPosNumFromUser(userMessage, !k_OnlyIntegerAllowed);
                        inputIsValid = true;
                    }
                    catch (Exception exception)
                    {
                        ConsoleUtilities.ShowBadInputMessage(exception.Message);
                    }
                }
            }
        }

        // Iterating on all features that have been defined, and ask for user to fill each feature seperately
        private void getAndUpdateFeaturesInfo(VehicleInfo i_NewVehicleInfo)
        {
            foreach (Feature feature in i_NewVehicleInfo.FeaturesList)
            {
                string inputFeatureValue;
                bool inputFeatureValid;
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

        // Asking user wether he wants to filter results, then shows the filtered/unfiltered list of registration numbers
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

        // helper Method to get from user an enum option of all listing and casting properly
        private VehicleListing.eVehicleStatus getVehicleStatus()
        {
            int userChoice = ConsoleUtilities.ChooseEnumValue(typeof(VehicleListing.eVehicleStatus));

            return (VehicleListing.eVehicleStatus)userChoice;
        }

        // helper Method to get from user an enum option of all listing and casting properly
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
           
            // There is no listing with current status filter
            if(!registrationNumberList.Any())
            {
                messageToUser.Append("Listing is empty!");
            }
            else
            {
                // preparing a string containing all reg. numbers
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
                ConsoleUtilities.PromptMessage(string.Format("All tires of vehicle ({0}) are now filled!", registrationNumber));
            }
            catch (Exception exception)
            {
                ConsoleUtilities.PromptMessage("Failed to fill tires. Info: " + exception.Message);   
            }
        }

        private void fillFuelTankOfAVehicle()
        {
            bool actionIsDone = false;
            string registrationNumber = this.getRegistrationNumber(eInputKeyConstraints.Existing);

            // while  fuel tank is filled successfully or bad use of action
            while (!actionIsDone)
            {
                try
                {
                    FuelEnergySource.eFuelType fuelTypeToFill = this.getFuelType();
                    string userMessage = "Please Enter the Amount of Litters to Fill ";
                    float fuelAmountToFill =
                        (float)ConsoleUtilities.GetPosNumFromUser(userMessage, !k_OnlyIntegerAllowed);
                    this.m_GarageSystem.FillFuelTankOfAVehicle(registrationNumber, fuelTypeToFill, fuelAmountToFill);
                    actionIsDone = true;
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    ConsoleUtilities.ShowBadInputMessage(valueOutOfRangeException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    ConsoleUtilities.PromptMessage(argumentException.Message);
                    actionIsDone = true;
                }
            }
        }

        private void chargeElectricalVehicle()
        {
            bool actionIsDone = false;
            string registrationNumber = this.getRegistrationNumber(eInputKeyConstraints.Existing);

            // while battery charged successfully or bad use of action
            while (!actionIsDone)
            {
                try
                {
                    string userMessage = "Please Enter Number Of Minutes To Charge";
                    int numOfMinutesToCharge =
                        (int)ConsoleUtilities.GetPosNumFromUser(userMessage, k_OnlyIntegerAllowed);
                    this.m_GarageSystem.ChargeElectricalVehicle(registrationNumber, numOfMinutesToCharge);
                    actionIsDone = true;
                }
                catch (ValueOutOfRangeException valueOutOfRangeException)
                {
                    ConsoleUtilities.ShowBadInputMessage(valueOutOfRangeException.Message);
                }
                catch (ArgumentException argumentException)
                {
                    ConsoleUtilities.PromptMessage(argumentException.Message);
                    actionIsDone = true;
                }
            }
        }

        // presenting to user all vehicle data
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
