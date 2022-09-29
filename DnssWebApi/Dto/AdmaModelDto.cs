using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnssWebApi.Dto
{
    public class AdmaModelDto
    {
        public float Default { get; set; }

        public int ID { get; set; }

        public bool Inaktiv { get; set; }

        public float Minimum { get; set; }

        public float Maximum { get; set; }

        public int Propertiers { get; set; }

        public string Res { get; set; }

        public string Type { get; set; }

        public float Resolution { get; set; }

        public string Unit { get; set; }

        public float Value { get; set; }
    }
}
