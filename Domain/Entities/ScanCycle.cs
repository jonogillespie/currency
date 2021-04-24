using System;
using System.Collections.Generic;
using Domain.Entities.Base;

namespace Domain.Entities
{
    public class ScanCycle : Entity
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public List<ScanResult> ScanResults { get; init; }
    }
}