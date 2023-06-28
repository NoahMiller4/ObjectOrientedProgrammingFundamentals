using System;

namespace Lab_4
{
    public class Client
    {
        public string Name { get; }
        public long CreditCard { get; }
        public List<Reservation> Reservations { get; }

        public Client(string name, long creditCard)
        {
            Name = name;
            CreditCard = creditCard;
            Reservations = new List<Reservation>();
        }
    }
}

