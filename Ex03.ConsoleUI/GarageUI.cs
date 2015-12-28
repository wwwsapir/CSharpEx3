using System.Text;
using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    using System.Globalization;
    using System.Linq;

    public class GarageUI // TODO: decide: maybe should be GarageUIManager
    {
        private const bool k_OnlyIntegerAllowed = true;
        private GarageSystemManager m_GarageSystem = new GarageSystemManager();
        private bool m_KeepRunning = true;

        internal enum eMenuOptions
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

        private static TEnum getHighestValueForEnum<TEnum>()
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().Max();
        }

        public void StartGarageManagementProgram()
        {
            Console.Clear();
            Console.WriteLine("Welcome to The Garage Mangament System");
            startRequestsIteration();
        }

        private void startRequestsIteration()
        {
            while (m_KeepRunning)
            {
                try
                {
                    showMenuAndHandleRequests();
                }
                catch (ValueOutOfRangeException exception)
                {
                    ConsoleUtilities.ShowBadInputMessage("Invalid choice: " + exception.Message);
                    continue;
                }

                if (m_KeepRunning)
                {
                    showMenuAndHandleRequests();
                }
            }
        }

        private void showMenuAndHandleRequests()
        {
                printMenuOptions();
                int userInput = (int)ConsoleUtilities.GetPosNumFromUser("Please choose action", k_OnlyIntegerAllowed);
                handleUserChoice(userInput - 1);
        }

        private void handleUserChoice(int i_UserInput)
        {
            eMenuOptions userChoice = (eMenuOptions)i_UserInput;

            switch (userChoice)
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
                    throw new ValueOutOfRangeException("Enum value does not Exists", 0, (float)getHighestValueForEnum<eMenuOptions>());
            }
        }

        private void printMenuOptions()
        {
            Console.WriteLine(@"
1. Add vehicle to the garage
2. List all registration numbers
3. Change vehicle state
4. Fill Vehicle Tires
5. Fill Fuel Tank
6. Charge Electrical Vehicle
7. Show vehicle information
8. Exit
");
        }

        private void addVehicleToSystem()
        {
            string newRegistrationNumber;
            OwnerInfo newOwnerInfo;
            VehicleInfo newVehicleInfo;

            while (true)
            {
                try
                {
                    newRegistrationNumber = getRegistrationNumber();
                    newVehicleInfo = getNewVehicleInfo();
                    newOwnerInfo = getNewOwnerInfo();
                    updateVehicleExtraInfo(newVehicleInfo);
                    this.m_GarageSystem.AddVehicleToDataBase(newRegistrationNumber, newVehicleInfo, newOwnerInfo);
                }
                catch (InvalidOperationException)
                { // key already exists
                    throw;
                }
                catch (FormatException)
                { // Input Format not valid

                    throw;
                }
                catch (ArgumentException)
                {  // Argument not valid

                    throw;
                }
                catch (Exception)
                {  // other

                    throw;
                }
            }
        }

        private OwnerInfo getNewOwnerInfo()
        {
            OwnerInfo newOwnerInfo;
            string ownerName;
            string ownerPhoneNumber;

            ownerName = ConsoleUtilities.GetInputString("Please Enter Owner Name");
            ownerPhoneNumber =
                ConsoleUtilities.GetPosNumFromUser("Please Enter Phone Number (Digits Only)", k_OnlyIntegerAllowed).ToString(CultureInfo.CurrentCulture);
            newOwnerInfo = new OwnerInfo(ownerName, ownerPhoneNumber);

            return newOwnerInfo;
        }

        private VehicleInfo getNewVehicleInfo()
        {
            string inputVehicleType;

            showVehicleTypesOptions();
            inputVehicleType = ConsoleUtilities.GetInputString("Please Enter Vehicle Type");
            VehicleCreator.eVehicleType parsedVehicleType = VehicleCreator.ParseVehicleType(inputVehicleType);

            return VehicleCreator.CreateVehicle(parsedVehicleType);
        }

        private void showVehicleTypesOptions()
        {
            StringBuilder allTypes = new StringBuilder("Available types : " + Environment.NewLine);

            foreach (string vehicleDesc in VehicleCreator.VehicleTypesDesc)
            {
                allTypes.Append(vehicleDesc + Environment.NewLine);
            }

            Console.WriteLine(allTypes);
        }

        private string getRegistrationNumber()
        {
            string newRegistrationNumber = ConsoleUtilities.GetInputString("Please Enter Registration Number");         
            return newRegistrationNumber;
        }

        private void updateVehicleExtraInfo(VehicleInfo i_NewVehicleInfo)
        {
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
            float batterHoursLeft =
                (float)
                ConsoleUtilities.GetPosNumFromUser("Please Enter Remainding Battery Hours", !k_OnlyIntegerAllowed);
            i_ElectricEnergySource.BatteryHoursLeft = batterHoursLeft;
        }

        private void getAndUpdateFuelEnergyData(FuelEnergySource i_FuelEnergySource)
        {
            float fuelLiterAmount =
                (float)
                ConsoleUtilities.GetPosNumFromUser("Please Enter Remainding Fuel Liters", !k_OnlyIntegerAllowed);
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
                userMessage = string.Format("Please Enter tire number {0}'s Air Pressure", tireIndex);
                tire.CurrAirPressure = (float)ConsoleUtilities.GetPosNumFromUser(userMessage, !k_OnlyIntegerAllowed);
            }
        }

        private void getAndUpdateFeaturesInfo(VehicleInfo i_NewVehicleInfo)
        {
            foreach (Feature feature in i_NewVehicleInfo.FeaturesList)
            {
                string userMessage = string.Format(
                    "Please enter {0}, in the given format : {1}",
                    feature.Description,
                    feature.PossibleValues);
                string inputFeatureValue = ConsoleUtilities.GetInputString(userMessage);
                feature.SetValue(inputFeatureValue);
            }
        }

        private void presentRegistrationNumbersList()
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

        private VehicleListing.eVehicleStatus getVehicleStatus()
        {
            string userMessage = string.Format("Please Choose Status: {0}", VehicleListing.VehicleStatusPossibleValues);
            string inputStatusString = ConsoleUtilities.GetInputString(userMessage);

            return VehicleListing.ParseVehicleStatus(inputStatusString);
        }

        private FuelEnergySource.eFuelType getFuelType()
        {
            string userMessage = string.Format("Please Choose Fuel type : {0}", FuelEnergySource.FuelTypePossibleValues);
            string inputStatusString = ConsoleUtilities.GetInputString(userMessage);

            return FuelEnergySource.ParseFuelType(inputStatusString);
        }

        private void presentRegistrationNumbersList(VehicleListing.eVehicleStatus? i_VehicleStatusToFilterBy)
        {
            StringBuilder allRegistrationNumberStr = new StringBuilder("Registration number Listing : " + Environment.NewLine);
            foreach (string registrationNumber in this.m_GarageSystem.GetRegistrationNumbersList(i_VehicleStatusToFilterBy))
            {
                allRegistrationNumberStr.Append(registrationNumber + Environment.NewLine);
            }

            Console.Write(allRegistrationNumberStr);
        }

        private void changeVehicleState()
        {
            string registrationNumber = this.getRegistrationNumber();
            VehicleListing.eVehicleStatus newStatus = this.getVehicleStatus();
            this.m_GarageSystem.ChangeVehicleState(registrationNumber, newStatus);
        }

        private void fillTiresOfAVehicleToMax()
        {
            string registrationNumber = this.getRegistrationNumber();
            this.m_GarageSystem.FillTiresOfAVehicleToMax(registrationNumber);
        }

        private void fillFuelTankOfAVehicle()
        {
            string registrationNumber = this.getRegistrationNumber();
            FuelEnergySource.eFuelType fuelTypeToFill = this.getFuelType();
            float fuelAmountToFill = (float)ConsoleUtilities.GetPosNumFromUser("Please Enter the Amount of Litters to Fill", !k_OnlyIntegerAllowed);
            this.m_GarageSystem.FillFuelTankOfAVehicle(registrationNumber, fuelTypeToFill, fuelAmountToFill);
        }

        private void chargeElectricalVehicle()
        {
            string registrationNumber = this.getRegistrationNumber();
            int numOfMinutesToCharge = (int)ConsoleUtilities.GetPosNumFromUser("Please Enter Number Of Minutes To Charge", k_OnlyIntegerAllowed);
            this.m_GarageSystem.ChargeElectricalVehicle(registrationNumber, numOfMinutesToCharge);
        }

        private void showVehicleData()
        {
            string registrationNumber = this.getRegistrationNumber();
            VehicleListing vehicleListing = this.m_GarageSystem.GetListing(registrationNumber);
            string messageToUser = string.Format(
@"
Vehicle Data:
Registration Number: {0}
{1}",
            registrationNumber,
            vehicleListing.ToString());
            ConsoleUtilities.PromptMessage(messageToUser);
        }
    }
}
