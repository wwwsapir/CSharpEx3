using System;

namespace Ex03.GarageLogic
{
    public sealed class MaxCarryWeightFeature : Feature
    {
        private float m_MaxCarryWeight;

        public MaxCarryWeightFeature()
        {
            m_Description = "Engine Capacity (in tons)";
            m_PossibleValues = "{ Decimal number in range 0..100 }";
        }

        public override void SetValue(string i_ValueStr)
        {
            float tempMaxCarryWeight;
            bool inputValid = float.TryParse(i_ValueStr, out tempMaxCarryWeight);
            if (!inputValid)
            {
                throw new FormatException();
            }
            else
            {
                if (tempMaxCarryWeight < 0 || tempMaxCarryWeight > 100)
                {
                    throw new ValueOutOfRangeException();
                }
            }
            m_MaxCarryWeight = tempMaxCarryWeight;
        }

        protected override object Value
        {
            get { return m_MaxCarryWeight; }
        }
    }
}
