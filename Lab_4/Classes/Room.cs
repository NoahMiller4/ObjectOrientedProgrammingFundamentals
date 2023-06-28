using System;

namespace Lab_4
{
    public class Room
    {
        public string Number { get; }
        public int Capacity { get; }
        public bool Occupied { get; }
        public List<Reservation> Reservations { get; }

        public Room(string number, int capacity)
        {
            Number = number;
            Capacity = capacity;
            Occupied = false;
            Reservations = new List<Reservation>();
        }
    }

}
