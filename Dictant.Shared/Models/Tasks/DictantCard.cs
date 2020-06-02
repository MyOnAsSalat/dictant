using System;
using System.Collections.Generic;
using System.Text;

namespace Dictant.Shared.Models.Tasks
{
    public class DictantCard
    {
        public int Id { get;set; }
        public DictantSource DictantSource {get;set; }
        public string OwnerId {get;set;}
        public string ApproverId {get;set;}
        public bool Approved { get;set;}
    }
}
