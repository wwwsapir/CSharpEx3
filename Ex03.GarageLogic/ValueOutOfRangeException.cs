using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_OutOfRangeValue;
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(string i_Message, float i_OutOfRangeValue, float i_MinValue, float i_MaxValue)
            : base(i_Message)
        {
            this.r_OutOfRangeValue = i_OutOfRangeValue;
            this.r_MinValue = i_MinValue;
            this.r_MaxValue = i_MaxValue;
        }

        public float MaxValue
        {
            get
            {
                return this.r_MaxValue;
            }
        }

        public float MinValue
        {
            get
            {
                return this.r_MinValue;
            }
        }

        public float OutOfRangeValue
        {
            get
            {
                return this.r_OutOfRangeValue;
            }
        }
    }
}
