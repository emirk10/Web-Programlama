﻿using System.ComponentModel.DataAnnotations;

namespace BarberApp.Models
{
    public class Barber
    {
        [Key]
        public int BarberID{ get; set; }
        [Required(ErrorMessage = "Barber name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Barber surname is required.")]
        public string Surname { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public string Specialization { get; set; }
        public string Rating { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public List<Schedule>? Schedules { get; set; }
    }
}