using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLagCompensationPoC
{
    public class Msg
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        //blank constructor
        public Msg()
        {
        }

        public Msg(int id, string name, string content)
        {
            Id = id;
            Name = name;
            Content = content;
        }
    }
}
