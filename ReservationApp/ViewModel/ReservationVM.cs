using ReservationApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationApp.ViewModel
{
    public class ReservationVM
    {
        public ReservationVM()
        {
           Reservations = new List<Reservation>();
            Reservation = new Reservation();
        }
        public Reservation Reservation { get; set; }
        public List<Reservation> Reservations { get; set; }
    }
}