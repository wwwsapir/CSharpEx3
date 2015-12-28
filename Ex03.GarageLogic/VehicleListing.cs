using System;

namespace Ex03.GarageLogic
{
    public sealed class VehicleListing
    {
        private static readonly string sr_VehicleStatusPossibleValues = "{InRepair/Repaired/Paid}";
        private OwnerInfo m_OwnerInfo;
        private VehicleInfo m_VehicleInfo;
        private eVehicleStatus m_VehicleStatus;

        public static string VehicleStatusPossibleValues
        {
            get
            {
                return sr_VehicleStatusPossibleValues;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set
            {
                this.m_VehicleStatus = value;
            }
        }

        public VehicleInfo VehicleInfo
        {
            get { return m_VehicleInfo; }
            set { m_VehicleInfo = value; }
        }

        public OwnerInfo Info
        {
            get
            {
                return this.m_OwnerInfo;
            }
        }

        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        public VehicleListing(VehicleInfo i_VehicleInfo, OwnerInfo i_OwnerInfo)
        {
            m_OwnerInfo = i_OwnerInfo;
            m_VehicleInfo = i_VehicleInfo;
            this.m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public static eVehicleStatus ParseVehicleStatus(string i_ValueStr)
        {
            eVehicleStatus returnedVehicleStatus;
            const bool v_IgnoreCaseDifferences = true;
            bool inputValid = Enum.TryParse(i_ValueStr, v_IgnoreCaseDifferences, out returnedVehicleStatus);
            if (!inputValid)
            {
                throw new FormatException();
            }

            return returnedVehicleStatus;
        }

        public override string ToString()
        {
            return this.m_OwnerInfo.ToString() + this.m_VehicleInfo.ToString() + " Vehicle State: " + this.m_VehicleStatus.ToString("G");
        }
    }
}
