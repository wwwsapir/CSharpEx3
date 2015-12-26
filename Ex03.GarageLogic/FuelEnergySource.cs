using System;

namespace Ex03.GarageLogic
{
    public sealed class FuelEnergySource : EnergySource
    {
        private const float k_MaxPossibleFuelLitersCapacityForAllTanks = 10000;

        eFuelType m_FuelType;
        float m_CurrFuelLitersAmount;
        float m_MaxFuelLitersAmount;

        public enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public float CurrFuelLitersAmount
        {
            get { return m_CurrFuelLitersAmount; }
        }

        public float MaxFuelLitersAmount
        {
            get { return m_MaxFuelLitersAmount; }
        }

        public void FillFuelAmountData(float i_MaxFuelLitersAmount, float i_CurrFuelLitersAmount)
        {
            if (i_MaxFuelLitersAmount > k_MaxPossibleFuelLitersCapacityForAllTanks)
            {
                string exceptionStr = string.Format(
                    "The max fuel amount is not valid. Max amount is {0} liters.",
                    k_MaxPossibleFuelLitersCapacityForAllTanks);
                throw new ValueOutOfRangeException(exceptionStr);
            }
            else if (i_MaxFuelLitersAmount <= 0 || i_CurrFuelLitersAmount < 0)
            {
                throw new ValueOutOfRangeException("At least one of the values is too small.");
            }
            else if (i_MaxFuelLitersAmount < i_CurrFuelLitersAmount)
            {
                throw new ValueOutOfRangeException("Fuel Liters to fill exceeds the max liters capacity.");
            }
            else
            {
                m_MaxFuelLitersAmount = i_MaxFuelLitersAmount;
                m_CurrFuelLitersAmount = i_CurrFuelLitersAmount;
                UpdateEnergyPercentageLeft();
            }
        }

        protected override void UpdateEnergyPercentageLeft()
        {
            m_EnergyPercentageLeft = m_CurrFuelLitersAmount / m_MaxFuelLitersAmount * 100;
        }

        public void FillFuelTank(eFuelType i_FuelType, float i_LitersToFill)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("The fuel type to fill doesn't match the vehicle's fuel type.");
            }
            else if (i_LitersToFill <= 0)
            {
                throw new ValueOutOfRangeException("The value has to be positive.");
            }
            else if (m_CurrFuelLitersAmount + i_LitersToFill > m_MaxFuelLitersAmount)
            {
                string exceptionStr = string.Format(
                    "The amount of fuel exceeds the max amount for this vehicle, current max amount to fill is: {0} liters.",
                    m_MaxFuelLitersAmount - m_CurrFuelLitersAmount);
                throw new ValueOutOfRangeException(exceptionStr);
            }
            else
            {
                m_CurrFuelLitersAmount += i_LitersToFill;
                UpdateEnergyPercentageLeft();
            }
        }
    }
}
