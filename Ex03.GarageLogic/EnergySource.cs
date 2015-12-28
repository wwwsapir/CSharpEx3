using System;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        protected float m_EnergyPercentageLeft;

        public float EnergyPercentageLeft
        {
            get { return m_EnergyPercentageLeft; }
        }

        protected abstract void UpdateEnergyPercentageLeft();

        public new abstract string ToString();
    }
}
