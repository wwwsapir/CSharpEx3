using System;

namespace Ex03.GarageLogic
{
    public class ColorFeature : Feature
    {
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
            m_Description = "Color";
            m_PossibleValues = "{Red/Blue/Black/White}";
        }

        public override void SetValue(string i_ValueStr)
        {
            const bool v_IgnoreCaseDifferences = true;
            bool inputValid = Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out m_Color);
            if (!inputValid)
            {
                throw new FormatException();
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", m_Description, m_Color);
        }
    }
}
