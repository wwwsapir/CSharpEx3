using System;

namespace Ex03.GarageLogic
{
    public sealed class ColorFeature : Feature
    {
        private eColor m_Color;

        private enum eColor 
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

        protected override object Value
        {
            get { return m_Color; }
        }
    }
}
