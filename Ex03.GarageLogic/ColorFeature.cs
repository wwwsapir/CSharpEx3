using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class ColorFeature : Feature
    {
        const bool k_IgnoreCaseDifferences = true;
        private eCarColor m_Color;

        private enum eCarColor 
        {
            Red,
            Blue,
            Black,
            White
        }

        public ColorFeature()
        {
            m_Description = "Color {Red/Blue/Black/White}";
        }

        public override void SetValue(string i_ValueStr)
        {
            eCarColor currCarColor;
            bool inputValid = Enum.TryParse(i_ValueStr, V_IgnoreCaseDifferences, out currCarColor);
            if (!inputValid)
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return m_Description + ": " + m_Color.ToString();
        }
    }
}
