﻿using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public int CustomerID{ get; set; }
        public Customer Customer{ get; set; }
        public int BarberID{ get; set; }
        public Barber Barber { get; set; }
        public List<ServiceAppointment> ServiceAppointments { get; set; }
        public int AdminID { get; set; }
    }
    public enum AppointmentStatus
    {
        Pending = 0,
        Confirmed = 1,
        Cancelled = 2
    }
}
