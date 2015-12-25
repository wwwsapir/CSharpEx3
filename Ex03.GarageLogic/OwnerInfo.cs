using System;

namespace Ex03.GarageLogic
{
    public sealed class OwnerInfo
    {
        string m_Name;
        string m_PhoneNumber;

        public OwnerInfo(string i_Name, string i_PhoneNumber)
        {
            m_Name = i_Name;
            m_PhoneNumber = i_PhoneNumber;
        }
    }
}
