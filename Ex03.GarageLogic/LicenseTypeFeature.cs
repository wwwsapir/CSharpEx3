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
            eLicenseType licenseType;
            const bool v_IgnoreCaseDifferences = true;
            if (!IsValid(i_ValueStr))
            {
                throw new FormatException("License type format is not valid");
            }

            Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out licenseType);
            this.m_LicenseType = licenseType;
        }

        public override bool IsValid(string i_InputFeatureValue)
        {
            const bool v_IgnoreCaseDifferences = true;
            eLicenseType dummyLicenseType;
            int dummyInt;
            bool parseSuccess = Enum.TryParse(i_InputFeatureValue, v_IgnoreCaseDifferences, out dummyLicenseType);
            bool isNumber = int.TryParse(i_InputFeatureValue, out dummyInt);

            return parseSuccess && !isNumber;
        }

        protected override object Value
        {
            get { return m_LicenseType; }
        }
    }
}
