using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4
{
    public class Pacient
    {
        public Type Type { get; set; }

        public Pacient(Type type)
        {
            Type = type;
        }
    }

    public enum Type
    {
        First = 1,
        Second = 2,
        Third = 3
    }
}