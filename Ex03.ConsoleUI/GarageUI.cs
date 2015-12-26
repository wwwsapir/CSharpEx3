using System;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageUI // TODO: decide: maybe should be GarageUIManager
    {
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

        public void StartGarageManagementProgram()
        {
            Console.WriteLine("Welcome to The Garage Mangament System");
            startRequestsIteration();
        }

        private void startRequestsIteration()
        {

            while (m_KeepRunning)
            {
                showMenuAndHandleRequests();
                if (m_KeepRunning)
                {
                    showMenuAndHandleRequests();
                }
            }
        }

        private void showMenuAndHandleRequests()
        {
            try
            {
                printMenuOptions();
                int userInput = ConsoleUtilities.GetPosNumFromUser("Please choose action");
                handleUserChoice(userInput - 1);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Choice is not valid");
            }
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
                    throw new ArgumentException();
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

        }

        private void presentRegistrationNumbersList()
        {

        }

        private void changeVehicleState()
        {

        }

        private void fillTiresOfAVehicleToMax()
        {

        }

        private void fillFuelTankOfAVehicle()
        {

        }

        private void chargeElectricalVehicle()
        {

        }

        private void showVehicleData()
        {

        }
    }
}
