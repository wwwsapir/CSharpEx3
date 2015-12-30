using System;

namespace Ex03.GarageLogic
{
    public sealed class MaxCarryWeightFeature : Feature
    {
        private const float k_MaxCarryWeightLimit = 100;
        private float m_MaxCarryWeight;

        public MaxCarryWeightFeature()
        {
            m_Description = "Engine Capacity (in tons)";
            this.m_PossibleValues = "{ Decimal number in range 0..100 }";
        }

        public override void SetValue(string i_ValueStr)
        {
            float tempMaxCarryWeight;
            bool inputValid = float.TryParse(i_ValueStr, out tempMaxCarryWeight);
            if (!inputValid)
            {
                throw new FormatException();
            }

            if (tempMaxCarryWeight < 0 || tempMaxCarryWeight > k_MaxCarryWeightLimit)
            {
                throw new ValueOutOfRangeException("Given Value For Max Carry Weight Out Of Range", tempMaxCarryWeight, 0, k_MaxCarryWeightLimit);
            }
            
            m_MaxCarryWeight = tempMaxCarryWeight;
        }

        public override bool IsValid(string i_InputFeatureValue)
        {
            float tempMaxCarryWeight;
            bool inputValid = float.TryParse(i_InputFeatureValue, out tempMaxCarryWeight);
            inputValid &= tempMaxCarryWeight >= 0 || tempMaxCarryWeight <= k_MaxCarryWeightLimit;

            return inputValid;
        }

        protected override object Value
        {
            get { return m_MaxCarryWeight; }
        }
    }
}
