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
            const bool v_IgnoreCaseDifferences = true;
            bool inputValid = Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out m_DoorsNumber);
            if (!inputValid)
            {
                throw new FormatException();
            }
        }

        protected override object Value
        {
            get { return m_DoorsNumber; }
        }
    }
}
