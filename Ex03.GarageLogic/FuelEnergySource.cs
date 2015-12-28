using System;

namespace Ex03.GarageLogic
{
    public sealed class FuelEnergySource : EnergySource
    {
        private static readonly string sr_FuelTypePossibleValues = "{Soler/Octan95/Octan96/Octan98}";
        private readonly float r_MaxFuelLitersAmount;
        private eFuelType m_FuelType;
        private float m_CurrFuelLitersAmount;

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public static string FuelTypePossibleValues
        {
            get
            {
                return sr_FuelTypePossibleValues;
            }
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }
        
        public float CurrFuelLitersAmount
        {
            get
            {
                return m_CurrFuelLitersAmount;
            }

            set
            {
                if (value > this.r_MaxFuelLitersAmount)
                {
                    string exceptionStr = string.Format(
                        "The given fuel amount is not valid. Max amount is {0} liters.",
                        this.r_MaxFuelLitersAmount);
                    throw new ValueOutOfRangeException(exceptionStr);
                }

                if (value < 0)
                {
                    throw new ValueOutOfRangeException("Fuel amount cannot be negative");
                }
               
                    m_CurrFuelLitersAmount = value;
                    UpdateEnergyPercentageLeft();
            }
        }

        public float MaxFuelLitersAmount
        {
            get { return this.r_MaxFuelLitersAmount; }
        }

        public FuelEnergySource(float i_MaxFuelLitersAmount, eFuelType i_FuelType)
        {
            if (i_MaxFuelLitersAmount <= 0)
            {
                throw new ValueOutOfRangeException("Max Fuels litters must be positive");
            }

            this.r_MaxFuelLitersAmount = i_MaxFuelLitersAmount;
            m_FuelType = i_FuelType;
        }

        protected override void UpdateEnergyPercentageLeft()
        {
            m_EnergyPercentageLeft = m_CurrFuelLitersAmount / this.r_MaxFuelLitersAmount * 100;
        }

        public void FillFuelTank(eFuelType i_FuelType, float i_LitersToFill)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("The fuel type to fill doesn't match the vehicle's fuel type.");
            }

            if (i_LitersToFill <= 0)
            {
                throw new ValueOutOfRangeException("The value has to be positive.");
            }

            if (m_CurrFuelLitersAmount + i_LitersToFill > this.r_MaxFuelLitersAmount)
            {
                string exceptionStr = string.Format(
                    "The amount of fuel exceeds the max amount for this vehicle, current max amount to fill is: {0} liters.",
                    this.r_MaxFuelLitersAmount - m_CurrFuelLitersAmount);
                throw new ValueOutOfRangeException(exceptionStr);
            }
            
            m_CurrFuelLitersAmount += i_LitersToFill;
            UpdateEnergyPercentageLeft();
        }

        public static eFuelType ParseFuelType(string i_ValueStr)
        {
            eFuelType returnedVehicleStatus;
            const bool v_IgnoreCaseDifferences = true;
            bool inputValid = Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out returnedVehicleStatus);
            if (!inputValid)
            {
                throw new FormatException();
            }

            return returnedVehicleStatus;
        }

        public override string ToString()
        {
            return string.Format(
@"Fuel Type: {0}
Fuel Left: {1}%",
                this.FuelType.ToString("G"),
                this.m_EnergyPercentageLeft);
        }
    }
}
