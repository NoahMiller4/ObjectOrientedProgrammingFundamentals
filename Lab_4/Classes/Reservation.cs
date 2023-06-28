using System;

namespace Lab_4
{
    public class Reservation
    {
        public DateTime Date { get; }
        public int Occupants { get; }
        public bool IsCurrent { get; }
        public Client Client { get; }
        public Room Room { get; }

        public Reservation(DateTime date, int occupants, Client client, Room room)
        {
            Date = date;
            Occupants = occupants;
            IsCurrent = true;
            Client = client;
            Room = room;
        }
    }
}

