﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dictant.Shared.Models.Tasks
{
    //DTO
    public class Assignment
    {
        //Определяет тип задания (форму проведения)
        public string Type { get;set;}
        public string Token { get; set; }
        public DictantSource DictantSource {get;set; }
    }
}
