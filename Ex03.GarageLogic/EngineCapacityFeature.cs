using System;

namespace Ex03.GarageLogic
{
    public sealed class EngineCapacityFeature : Feature
    {
        private const int k_EngineCapacityLimit = 10000;
        private int m_EngineCapacity;

        public EngineCapacityFeature()
        {
            m_Description = "Engine Capacity (in cubic centimeters)";
            this.m_PossibleValues = "{ Whole number in range 1..10,000 }";
        }

        public override void SetValue(string i_ValueStr)
        {
            int tempEngineCapacity;
            bool inputValid = int.TryParse(i_ValueStr, out tempEngineCapacity);

            if (!inputValid)
            {
                throw new FormatException();
            }

            if (tempEngineCapacity < 1 || tempEngineCapacity > k_EngineCapacityLimit)
            {
                throw new ValueOutOfRangeException("Given Engine capacity exceed limit", tempEngineCapacity, 1, k_EngineCapacityLimit);
            }

            m_EngineCapacity = tempEngineCapacity;
        }

        public override bool IsValid(string i_InputFeatureValue)
        {
            int tempEngineCapacity;
            bool inputValid = int.TryParse(i_InputFeatureValue, out tempEngineCapacity);
            inputValid &= tempEngineCapacity >= 1 && tempEngineCapacity <= k_EngineCapacityLimit;

            return inputValid;
        }

        protected override object Value
        {
            get { return m_EngineCapacity; }
        }
    }
}
