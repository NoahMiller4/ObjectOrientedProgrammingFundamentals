using System;

namespace Lab_4
{
    public class Hotel
    {
        public string Name { get; }
        public string Address { get; }
        public List<Room> Rooms { get; }

        public List<Client> Clients { get; }

        public Hotel(string name, string address)
        {
            Name = name;
            Address = address;
            Rooms = new List<Room>();
            Clients = new List<Client>();
        }
    }

}
