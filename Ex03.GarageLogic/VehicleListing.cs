namespace Ex03.GarageLogic
{
    using System;

    public sealed class VehicleListing
    {
        private OwnerInfo m_OwnerInfo;
        private VehicleInfo m_VehicleInfo;
        private eVehicleStatus m_VehicleStatus;

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

        public override string ToString()
        {
            return string.Format("{0}{1}Vehicle State: {2}{3}", this.m_OwnerInfo, this.m_VehicleInfo, this.m_VehicleStatus, Environment.NewLine);
        }
    }
}
