using System;

namespace Ex03.GarageLogic
{
    public sealed class DangerousComponentsCarryFeature : Feature
    {
        private bool m_ContainsDangeroudComponents;

        public DangerousComponentsCarryFeature()
        {
            m_Description = "Dangerous Components";
            this.m_PossibleValues = "{Yes/No}";
        }

        public override void SetValue(string i_ValueStr)
        {
            if (!IsValid(i_ValueStr))
            {
                throw new FormatException();
            }

            i_ValueStr = i_ValueStr.ToLower().Trim();
            if (i_ValueStr.ToLower() == "yes")
            {
                m_ContainsDangeroudComponents = true;
            }
            else if (i_ValueStr.ToLower() == "no")
            {
                m_ContainsDangeroudComponents = false;
            }
        }

        public override bool IsValid(string i_InputFeatureValue)
        {
            string formattedInput = i_InputFeatureValue.ToLower().Trim();

            return formattedInput == "yes" || formattedInput == "no";
        }

        protected override object Value
        {
            get
            {
                string toReturn;
                if (m_ContainsDangeroudComponents == true)
                {
                    toReturn = "yes";
                }
                else   // m_ContainsDangeroudComponents == false
                {
                    toReturn = "no";
                }
                return toReturn;
            }
        }
    }
}
