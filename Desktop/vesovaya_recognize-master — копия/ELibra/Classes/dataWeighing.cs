using System;
using System.Runtime.Serialization;

namespace ELibra.Classes
{
    [DataContract]
    public class dataWeighing
    {
        [DataMember]
        public int id;
        [DataMember]
        public string clientId; 
        [DataMember]
        public string material;
        [DataMember]
        public int carWeight;
        [DataMember]
        public int brutto;
        [DataMember]
        public string dateWeight;
        [DataMember]
        public string carNumber;
        [DataMember]
        public string carMark;
        [DataMember]
        public int docWeight;
        [DataMember]
        public string point2;
        [DataMember]
        public string operationType;
        [DataMember]
        public int factorWeight;
        [DataMember]
        public double volume;
        [DataMember]
        public string driverid;
        [DataMember]
        public string storageid;
        [DataMember]
        public string dateOut;
        [DataMember]
        public string orderPositionNumber;
        [DataMember]
        public double adjustment;
        [DataMember]
        public string operatorId;
        [DataMember]
        public string taraScale;
        [DataMember]
        public string fullWeightScale;
        [DataMember]
        public string edited;
        [DataMember]
        public string redactor;
    }
}
