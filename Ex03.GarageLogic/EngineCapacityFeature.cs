using System;

namespace Ex03.GarageLogic
{
    public sealed class EngineCapacityFeature : Feature
    {
        private int m_EngineCapacity;

        public EngineCapacityFeature()
        {
            m_Description = "Engine Capacity (in cubic centimeters)";
            m_PossibleValues = "{ Whole number in range 1..10,000 }";
        }

        public override void SetValue(string i_ValueStr)
        {
            int tempEngineCapacity;
            bool inputValid = int.TryParse(i_ValueStr, out tempEngineCapacity);
            if (!inputValid)
            {
                throw new FormatException();
            }
            else
            {
                if (tempEngineCapacity < 1 || tempEngineCapacity > 10000)
                {
                    throw new ValueOutOfRangeException();
                }
            }
            m_EngineCapacity = tempEngineCapacity;
        }

        protected override object Value
        {
            get { return m_EngineCapacity; }
        }
    }
}
