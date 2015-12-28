using System;

namespace Ex03.GarageLogic
{
    public sealed class LicenseTypeFeature : Feature
    {
        private eLicenseType m_LicenseType;

        private enum eLicenseType
        {
            A,
            A1,
            A4,
            C
        }

        public LicenseTypeFeature()
        {
            m_Description = "License Type";
            this.m_PossibleValues = "{A/A1/A4/C}";
        }

        public override void SetValue(string i_ValueStr)
        {
            const bool v_IgnoreCaseDifferences = true;
            bool inputValid = Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out m_LicenseType);
            if (!inputValid)
            {
                throw new FormatException();
            }
        }

        protected override object Value
        {
            get { return m_LicenseType; }
        }
    }
}
