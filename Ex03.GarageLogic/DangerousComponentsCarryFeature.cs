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
            i_ValueStr = i_ValueStr.Trim();

            if (i_ValueStr.ToLower() == "yes")
            {
                m_ContainsDangeroudComponents = true;
            }
            else if (i_ValueStr.ToLower() == "no")
            {
                m_ContainsDangeroudComponents = false;
            }
            else
            {
                throw new FormatException();
            }
        }

        protected override object Value
        {
            get { return m_ContainsDangeroudComponents; }
        }
    }
}
