using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class VehicleInfo
    {
        private string m_VehicleType;
        private string m_ModelName;
        private EnergySource m_EnergySource;
        private List<Tire> m_TiresList;
        private List<Feature> m_FeaturesList;

        public VehicleInfo(string i_VehicleType)
        {
            m_VehicleType = i_VehicleType;      //UI already checked via VehicleCreator function that the vehicle type is valid
            GarageSystemManager.m_VehicleCreator.CreateVehicle(this, i_VehicleType);
        }

        public void FillTiresToMax()
        {
            foreach (Tire tire in m_TiresList)
            {
                tire.FillAirToMax();
            }
        }

        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public EnergySource EnergySource
        {
            get { return m_EnergySource; }
            set { m_EnergySource = value; }
        }

        public List<Tire> TiresList
        {
            get { return m_TiresList; }
            set { m_TiresList = value; }
        }

        public List<Feature> FeaturesList
        {
            get { return m_FeaturesList; }
            set { m_FeaturesList = value; }
        }
    }
}
