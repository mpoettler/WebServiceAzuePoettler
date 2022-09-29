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
        public float Default { get; set; }

        [Required]
        public int ID { get; set; }

        [Required]
        public bool Inaktiv { get; set; }

        [Required]
        public float Minimum { get; set; }

        [Required]
        public float Maximum { get; set; }

        [Required]
        public string Propertiers { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public float Resolution { get; set; }

        [Required]
        public string Unit { get; set; }

        [Required]
        public float Value { get; set; }

    }
}
