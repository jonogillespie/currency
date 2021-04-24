using System.Collections.Generic;
using Domain.Entities.Base;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Domain.Entities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Website : Entity
    {
        public string Url { get; set; }
        public string Name { get; set; }

        public List<ScanResult> ScanResults { get; set; }
    }
}