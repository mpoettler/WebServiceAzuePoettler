using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnssWebApi.Dto
{
    public class AdmaModelDto
    {
        public int ID { get; set; }

        public int Minimum { get; set; }

        public int Maximum { get; set; }
        public int Value { get; set; }
    }
}
