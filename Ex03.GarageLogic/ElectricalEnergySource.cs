namespace Ex03.GarageLogic
{
    public sealed class ElectricalEnergySource : EnergySource
    {
        private const float k_MaxPossibleBatteryHoursForAllBatteries = 500;

        float m_BatteryHoursLeft;
        float m_MaxBatteryHours;

        public float BatteryHoursLeft
        {
            get { return m_BatteryHoursLeft; }
        }

        public float MaxBatteryHours
        {
            get { return m_MaxBatteryHours; }
        }

        public void FillBatteryData(float i_MaxBatteryHours, float i_BatteryHoursLeft)
        {
            if (i_MaxBatteryHours > k_MaxPossibleBatteryHoursForAllBatteries)
            {
                string exceptionStr = string.Format(
                    "The max bettery time is not valid. Max time is {0} hours.",
                    k_MaxPossibleBatteryHoursForAllBatteries);
                throw new ValueOutOfRangeException(exceptionStr);
            }
            else if (i_MaxBatteryHours <= 0 || i_BatteryHoursLeft < 0)
            {
                throw new ValueOutOfRangeException("At least one of the values is too small.");
            }
            else if (i_MaxBatteryHours < i_BatteryHoursLeft)
            {
                throw new ValueOutOfRangeException("Time to fill exceeds the max battery time.");
            }
            else
            {
                m_MaxBatteryHours = i_MaxBatteryHours;
                m_BatteryHoursLeft = i_BatteryHoursLeft;
                UpdateEnergyPercentageLeft();
            }
        }

        protected override void UpdateEnergyPercentageLeft()
        {
            m_EnergyPercentageLeft = m_BatteryHoursLeft / m_MaxBatteryHours * 100;
        }

        public void FillBattery(int i_MinutesToCharge)
        {
            float hoursToCharge = (float)i_MinutesToCharge / 60;
            if (i_MinutesToCharge <= 0)
            {
                throw new ValueOutOfRangeException("The value has to be positive.");
            }
            else if (m_BatteryHoursLeft + hoursToCharge > m_MaxBatteryHours)
            {
                string exceptionStr = string.Format(
                    "The time to charge exceeds the max charge time for this vehicle, current max time to charge is {0} minutes.",
                    (int)((m_MaxBatteryHours - m_BatteryHoursLeft) * 60));
                throw new ValueOutOfRangeException(exceptionStr);
            }
            else
            {
                m_BatteryHoursLeft += hoursToCharge;
                UpdateEnergyPercentageLeft();
            }
        }
    }
}
