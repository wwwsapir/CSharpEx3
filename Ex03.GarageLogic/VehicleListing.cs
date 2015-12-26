using System;

namespace Ex03.GarageLogic
{
    public sealed class VehicleListing
    {
        private OwnerInfo m_OwnerInfo;
        private VehicleInfo m_VehicleInfo;
        private eVehicleStatus m_VehicleStatus;

        public eVehicleStatus VehicleStatus
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public VehicleInfo VehicleInfo
        {
            get { return m_VehicleInfo; }
            set { m_VehicleInfo = value; }
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
        }
    }
}
