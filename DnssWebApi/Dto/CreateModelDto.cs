using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DnssWebApi.Dto
{
    public class CreateModelDto
    {

        [Required]
        public int ID { get; set; }

        [Required]
        public int Minimum { get; set; }

        [Required]
        public int Maximum { get; set; }

        [Required]
        public int Value { get; set; }

    }
}
