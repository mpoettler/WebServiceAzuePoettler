/**
 * Web Service Development -  FH Joanneum SS22
 * Project: Rest Azure
 * @author  Matthias Pöttler
 * @version 1.0
 * @date   4/10/2022
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DnssWebApi.Model
{
    public class AdmaModel
    {

        public int ID { get; set; }

        public int Minimum { get; set; }

        public int Maximum { get; set; }

        public int Value { get; set; }
    }
}
