using System;

namespace Ex03.GarageLogic
{
    public sealed class FuelEnergySource : EnergySource
    {
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
                    throw new ValueOutOfRangeException(exceptionStr, value, 0, MaxFuelLitersAmount);
                }

                if (value < 0)
                {
                    throw new ValueOutOfRangeException("Fuel amount cannot be negative", value, 0, MaxFuelLitersAmount);
                }
               
                    m_CurrFuelLitersAmount = value;
                    UpdateEnergyPercentageLeft();
            }
        }

        public float MaxFuelLitersAmount
        {
            get { return this.r_MaxFuelLitersAmount; }
        }

        public float MaxFuelLitersAmountToFill
        {
            get { return this.r_MaxFuelLitersAmount - this.m_CurrFuelLitersAmount; }
        } 

        public FuelEnergySource(float i_MaxFuelLitersAmount, eFuelType i_FuelType)
        {
            if (i_MaxFuelLitersAmount <= 0)
            {
                throw new ArgumentException("Maximum capacity for fuel must be a positive number");
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

            if (i_LitersToFill < 0)
            {
                throw new ValueOutOfRangeException("Fuel amount to fill tank has to be non-negative", i_LitersToFill, 0, MaxFuelLitersAmount);
            }

            if (m_CurrFuelLitersAmount + i_LitersToFill > this.r_MaxFuelLitersAmount)
            {
                throw new ValueOutOfRangeException("The amount of fuel exceeds the max amount for this vehicle", i_LitersToFill, 0, this.r_MaxFuelLitersAmount - m_CurrFuelLitersAmount);
            }
            
            m_CurrFuelLitersAmount += i_LitersToFill;
            UpdateEnergyPercentageLeft();
        }

        public override string ToString()
        {
            return string.Format(
@"Fuel Type: {0}
Fuel Left: {1}%
",
                this.FuelType.ToString("G"),
                this.m_EnergyPercentageLeft);
        }
    }
}
