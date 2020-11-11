using StAbraamFamily.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StAbraamFamily.ViewModels
{
    public class NewFamily
    {
        public Person person { get; set; }
        public Family family { get; set; }
    }
}