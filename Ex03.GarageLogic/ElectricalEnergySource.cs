namespace Ex03.GarageLogic
{
    using System;

    public sealed class ElectricalEnergySource : EnergySource
    {
        private readonly float r_MaxBatteryHours;
        private float m_BatteryHoursLeft;

        public float BatteryHoursLeft
        {
            get { return m_BatteryHoursLeft; }

            set
            {
                if (value > this.r_MaxBatteryHours)
                {
                    string exceptionStr = string.Format(
                        "The given bettery time is not valid. Max time is {0} hours.",
                        this.r_MaxBatteryHours);
                    throw new ValueOutOfRangeException(exceptionStr, value, 0, this.r_MaxBatteryHours);
                }

                if (value < 0)
                {
                    throw new ValueOutOfRangeException("Hours left till battery drains cannot be negative.", value, 0, this.r_MaxBatteryHours);
                }
                
                m_BatteryHoursLeft = value;
                UpdateEnergyPercentageLeft();
            }
        }

        public float MaxBatteryHours
        {
            get { return this.r_MaxBatteryHours; }
        }

        public float MaxBatteryHoursToFill
        {
            get
            {
                return this.r_MaxBatteryHours - this.m_BatteryHoursLeft;
            }
        }

        protected override void UpdateEnergyPercentageLeft()
        {
            m_EnergyPercentageLeft = m_BatteryHoursLeft / this.r_MaxBatteryHours * 100;
        }

        public ElectricalEnergySource(float i_MaxBatteryHours)
        {
            if (i_MaxBatteryHours <= 0)
            {
                throw new ArgumentException("Max Battery hours must be positive");
            }

            this.r_MaxBatteryHours = i_MaxBatteryHours;
        }

        public void FillBattery(int i_MinutesToCharge)
        {
            float hoursToCharge = (float)i_MinutesToCharge / 60;
            if (i_MinutesToCharge <= 0)
            {
                throw new ValueOutOfRangeException("The value has to be positive.", i_MinutesToCharge, 0, this.r_MaxBatteryHours);
            }

            if (m_BatteryHoursLeft + hoursToCharge > this.r_MaxBatteryHours)
            {
                string exceptionStr = string.Format(
                    "The time to charge exceeds the max charge time for this vehicle, current max time to charge is {0} minutes.",
                    (int)((this.r_MaxBatteryHours - m_BatteryHoursLeft) * 60));
                throw new ValueOutOfRangeException(exceptionStr, i_MinutesToCharge, 0, (int)((this.r_MaxBatteryHours - m_BatteryHoursLeft) * 60));
            }
            
            m_BatteryHoursLeft += hoursToCharge;
            UpdateEnergyPercentageLeft();
        }

        public override string ToString()
        {
            return string.Format(
@"Battery State: {0}%
",
 this.m_EnergyPercentageLeft);
        }
    }
}
