using System;

namespace Ex03.GarageLogic
{
    public sealed class DoorsNumberFeature : Feature
    {
        private eDoorsNumber m_DoorsNumber;

        private enum eDoorsNumber
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        public DoorsNumberFeature()
        {
            m_Description = "Doors Number";
            this.m_PossibleValues = "{Two/Three/Four/Five}";
        }

        public override void SetValue(string i_ValueStr)
        {
            eDoorsNumber doorsNumber;
            const bool v_IgnoreCaseDifferences = true;

            if (!IsValid(i_ValueStr))
            {
                throw new FormatException();
            }

            Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out doorsNumber);
            this.m_DoorsNumber = doorsNumber;
        }

        public override bool IsValid(string i_InputFeatureValue)
        {
            const bool v_IgnoreCaseDifferences = true;
            eDoorsNumber dummyDoorsNumber;
            int dummyInt;
            bool parseSuccess = Enum.TryParse(i_InputFeatureValue, v_IgnoreCaseDifferences, out dummyDoorsNumber);
            bool isNumber = int.TryParse(i_InputFeatureValue, out dummyInt);

            return parseSuccess && !isNumber;
        }

        protected override object Value
        {
            get { return m_DoorsNumber; }
        }
    }
}
