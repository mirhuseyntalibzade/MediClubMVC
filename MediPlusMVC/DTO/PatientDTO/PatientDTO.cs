﻿namespace MediPlusMVC.DTO.PatientDTO
{
    public class PatientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Fincode { get; set; }
        public string PhoneNumber { get; set; }
        public bool isActive { get; set; }
    }
}
