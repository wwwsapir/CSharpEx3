using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ColorFeature : Feature
    {
        const bool V_IgnoreCaseDifferences = true;
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
            return m_Description + ": " + currCarColor.ToString();
        }
    }
}
