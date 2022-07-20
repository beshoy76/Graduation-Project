using HospitalManegement.Models;
using Microsoft.EntityFrameworkCore;
using Modelsidentiy.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManegement.IServices
{
    public interface IDepartmendServices
    {
        Task<List<medicine>> listofmedicines();
        Task<List<medicine>> listofmedicinesByName(String name);
        Task<List<medicine>> listofmedicinesById(String id);
        Task<List<prescrption>> listofprescrptionById(int id);

        void deleteMedicineByID(int id);
        medicine addmedicine( string patientId, String medicineName, string phone, string quantity, String exprDate);
        Task<string> editmedicine(int id, string patientId, string medicineName, string phone, string quantity, String exprDate);
        Task<string> editpriscription(int id, string patientId, string medicineName, string notes, string datetime , string department);

        prescrption addPriscription(string datetime, string medicineName, string notes, string patientId, string department);
        //, string patientId, String medicineName, string phone, string quantity, String exprDate
        Task<List<prescrption>> listofprescrption(string patientid);
         void deleteprescrptionByID(int id);

        Patient returPatient(string Id); 
        Doctor returndoctor(string Id);
        Task<List<Department>> listofDepartment();
        Task<List<Doctor>> listofdoctors();
        Task<List<Bed>> listavailableBeds();
        Task<List<Bed>> listallBeds();
        Task<string> bookingbed(string patientid, string bedid);
        Task<List<bookingBed>> listBookedbeds();
        Task<List<Doctor>> listofdoctorindepartment(int id);
        void deletedepartment(int id);
        Department addDepartment(String Name,string hospitalid);
        Task<string> Cancellation(string patientid, string bedid);
        Task<List<Hospitals>> Listofhospitals();
        Task<List<Department>> departinhospital(string id);
     //   object addPriscription(string datetime, List<string> medicineName, string notes, string patientId);
      //  Task editmedicine(int id, string patientId, string medicineName, string phone, string quantity, string exprDate);
    }



    // al lfunc 5 

    public class Departmentservices : IDepartmendServices
    {
        private readonly HositalContext context;

        public Departmentservices(HositalContext context)
        {
            this.context = context;
        }


        public async Task<List<Department>> listofDepartment()
        {
            var listofdepartment = await context.Departments.ToListAsync();

            return listofdepartment;

        }
        public Department addDepartment(String Name, string hospitalid)
        {
            var hospital = context.Hospitals
                .Where(x => x.id == hospitalid).SingleOrDefault();




            var department = new Department
            {

                name = Name,
                Hospitals = hospital


            };

            context.Departments.Add(department);
            context.SaveChanges();
            return department;

        }
        public void deletedepartment(int id)
        {


            var department = context.Departments.Find(id);

            context.Departments.Remove(department);
            context.SaveChanges();





        }

        public async Task<List<Doctor>> listofdoctors()
        {
            var listofdoctors = await context.Doctors.Include(x => x.Hospitals).Include(x => x.department)
                //.Select(x=>new  { name = x.name, DAge = x.DAge, Day= x.Day, DAddress=x.DAddress, DLastName=x.DLastName, DPhone=x.DPhone, DGender=x.DGender })
                .ToListAsync();


            return listofdoctors;

        }

        public async Task<List<Doctor>> listofdoctorindepartment(int id)
        {

            var doctors = await context.Doctors.Where(x => x.department.id == id).Include(x => x.Hospitals).ToListAsync();
            if (doctors == null)
            {
                new Errormodel
                {
                    Message = "no Department Here try again "
                };

            }

            return doctors;
        }

        public async Task<List<Bed>> listavailableBeds()
        {
            var listavailable = await

                context.Beds.Where(x => x.available == true).Include(x => x.Hospitals).ToListAsync();
            return listavailable;
        }

        public async Task<List<Bed>> listallBeds()
        {
            var listbeds = await context.Beds.Include(x => x.Hospitals)
                   .ToListAsync();


            return listbeds;
        }

        public async Task<string> bookingbed(string patientid, string bedid)
        {


            var patient = await context.Patients.Where(x => x.id == patientid).SingleOrDefaultAsync();
            if (patient == null)
            {
                return "No patient has this id";
            }
            var bed = await context.Beds.Where(x => x.id == bedid).SingleOrDefaultAsync();
            if (bed == null)
            {
                return "No bed has this id";
            }
            //var exist = context.BookingBeds
            //    .Where(x => x.patientid == patient.id)
            //    .SingleOrDefaultAsync();
            //if (exist != null)
            //{
            //    return "already booked";
            //}
            var booking = new bookingBed
            {
                bedid = bed.id,
                patientid = patient.id

            };
            bed.available = false;
            context.Add(booking);
            context.SaveChanges();


            return "successfully booking";
        }

        public async Task<List<bookingBed>> listBookedbeds()
        {
            var listbooked = await context.BookingBeds.ToListAsync();

            return listbooked;
        }



        public async Task<string> Cancellation(string patientid, string bedid)
        {

            var cancel = await context.BookingBeds
                .Where(p => p.bedid == bedid && p.patientid == patientid)
                .SingleOrDefaultAsync();
            var bed = await context.Beds
                .Where(x => x.id == bedid).SingleOrDefaultAsync();
            bed.available = true;
            context.Remove(cancel);
            context.SaveChanges();
            return "successfully cancel";
        }

        public async Task<List<Hospitals>> Listofhospitals()
        {
            var list = await context.Hospitals.ToListAsync();
            return list;
        }

        public async Task<List<Department>> departinhospital(string id)
        {
            var list = await context.Departments
                .Where(x => x.Hospitals.id == id).Include(x => x.Hospitals)
                .ToListAsync();

            return list;
        }



        

        // medicine

        public async Task<List<medicine>> listofmedicines()
        {
            var list = await context.Medicines.ToListAsync();
            return list;
        }

        public async Task<List<medicine>> listofmedicinesByName(string name)
        {

            //Include(x => x.medicineName).
            var list = await context.Medicines
                .Where(x => x.medicineName == name).ToListAsync();


            if (list == null)
            {
                new Errormodel
                {
                    Message = "no medicine Here try again "
                };

            }

            return list;
        }

        public async Task<List<medicine>> listofmedicinesById(string id)
        {

            //Include(x => x.medicineName).
            var list = await context.Medicines
                .Where(x => x.patientid == id).ToListAsync();


            if (list == null)
            {
                new Errormodel
                {
                    Message = "no medicine Here try again "
                };

            }

            return list;
        }


        public async Task<List<prescrption>> listofprescrptionById(int id)
        {

            //Include(x => x.medicineName).
            var list = await context.Prescrptions
                .Where(x => x.Id == id).ToListAsync();


            if (list == null)
            {
                new Errormodel
                {
                    Message = "no medicine Here try again "
                };

            }

            return list;
        }

        public medicine addmedicine(string patientId, string medicineName, string phone, string quantity, string ExprDate)
        {
            var medecine = new medicine
            {

                patientid = patientId,
                medicineName = medicineName,
                Phone = phone,
                Quantity = quantity,
                ExprDate = ExprDate

            };

            context.Medicines.Add(medecine);
            context.SaveChanges();
            return medecine;
        }

        public void deleteMedicineByID(int id)
        {
            var medicine = context.Medicines.Find(id);

            context.Medicines.Remove(medicine);
            context.SaveChanges();
        }


        // dont work
        //, string patientId, String medicineName, string phone, string quantity, String exprDate
  
        public async Task<string> editmedicine(int id ,string patientId, string medicineName, string phone, string quantity, string exprDate)
        {


            var result = await context.Medicines.FirstOrDefaultAsync(n => n.MedicineId == id);
            if (result != null)
            {
                //result.MedicineId = id;
                result.patientid = patientId;
                result.medicineName = medicineName;
                result.Phone = phone;
                result.Quantity = quantity;
                result.ExprDate = exprDate;


                await context.SaveChangesAsync();

                return result.ToString();
            }

            return "message error";
        }



        public async Task<List<prescrption>> listofprescrption(string patientid)
        {


            var listofprescrption = await context.Prescrptions.Where(n => n.patientid == patientid).ToListAsync();
            return listofprescrption;
        }

        public void deleteprescrptionByID(int id)
        {
            var Prescrptions = context.Prescrptions.Find(id);

            context.Prescrptions.Remove(Prescrptions);
            context.SaveChanges();
        }

        public prescrption addPriscription(string datetime, string medicineName, string notes, string patientId ,string department)
        {

            var prescriptionvar = new prescrption
            {

                patientid = patientId,
                medicineName = medicineName,
                notes = notes,
                dateTime = datetime,
                department = department
                
               

            };

            context.Prescrptions.Add(prescriptionvar);
            context.SaveChanges();
            return prescriptionvar;

        }

        public async Task<string> editpriscription(int id, string patientId, string medicineName, string notes, string datetime, string department)
        {
            var result = await context.Prescrptions.FirstOrDefaultAsync(n => n.Id == id);
            if (result != null)
            {
                //result.MedicineId = id;
                result.patientid = patientId;
                result.medicineName = medicineName;
                result.notes = notes;
                result.dateTime = datetime;
                result.department = department;



                await context.SaveChangesAsync();

                return result.ToString();
            }

            return "message error";
        }

        public Doctor returndoctor(string Id)
        {
            var doctor = context.Doctors.Where(x => x.id == Id).Include(x=>x.department).SingleOrDefault();
            return doctor;
        }

        public Patient returPatient(string Id)
        {
            var patient = context.Patients.Where(x => x.id == Id).SingleOrDefault();
            return patient;
        }


    }
}
