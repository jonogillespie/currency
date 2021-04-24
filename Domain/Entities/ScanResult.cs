using System;
using Domain.Entities.Base;

namespace Domain.Entities
{
    public class ScanResult : Entity
    {
        public Website Website { get; set; }
        public Guid WebsiteId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool HasGoogleAnalytics { get; set; }
        public ScanCycle ScanCycle { get; set; }
        public Guid ScanCycleId { get; set; }
    }
}