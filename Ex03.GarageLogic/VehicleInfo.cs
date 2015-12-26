using System;
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
            GarageSystemManager.m_VehicleCreator.CreateVehicle(this, i_VehicleType);
        }

        public void FillTiresToMax()
        {
            foreach (Tire tire in m_TiresList)
            {
                tire.FillAirToMax();
            }
        }
    }
}
