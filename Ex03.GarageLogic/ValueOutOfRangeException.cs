using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private readonly float r_MaxValue;
        private readonly float r_MinValue;

        public ValueOutOfRangeException(string i_Message, float i_MinValue, float i_MaxValue)
            : base(i_Message)
        {
            this.r_MinValue = i_MinValue;
            this.r_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(
            string i_Message,
            float i_MinValue,
            float i_MaxValue,
            Exception i_InnerException)
            : base(i_Message, i_InnerException)
        {
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
    }
}
