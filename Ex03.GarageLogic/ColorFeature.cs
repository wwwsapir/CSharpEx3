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
            this.m_PossibleValues = "{Red/Blue/Black/White}";
        }

        public override void SetValue(string i_ValueStr)
        {
            eColor color;
            const bool v_IgnoreCaseDifferences = true;
            if (!IsValid(i_ValueStr))
            {
                throw new FormatException();
            }

            Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out color);
            this.m_Color = color;
        }

        public override bool IsValid(string i_InputFeatureValue)
        {
            const bool v_IgnoreCaseDifferences = true;
            eColor dummyColor;
            int dummyInt;
            bool parseSuccess = Enum.TryParse(i_InputFeatureValue, v_IgnoreCaseDifferences, out dummyColor);
            bool isNumber = int.TryParse(i_InputFeatureValue, out dummyInt);

            return parseSuccess && !isNumber;
        }

        protected override object Value
        {
            get { return m_Color; }
        }
    }
}
