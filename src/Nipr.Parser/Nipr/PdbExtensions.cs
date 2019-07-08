using System;
using System.Collections.Generic;
using System.Linq;

namespace Nipr.Parser.Nipr
{
    public static class PdbExtensions
    {
        public static void AddAddress(this Pdb pdb, Address address)
        {
            if (string.IsNullOrEmpty(address.State)) throw new ArgumentException(Messages.AddressMissingState);
            pdb.AddAddress(address.State, address);
        }

        public static void AddAddress(this Pdb pdb, string state, Address address)
        {
            if (string.IsNullOrEmpty(state)) throw new ArgumentException(Messages.AddressMissingState);
            if (string.IsNullOrEmpty(address.State)) address.State = state;

            var addresses = pdb.GetStateAddress(state);

            if (addresses == null)
            {
                addresses = new StateAddress { State = state };
                pdb.AddStateAddress(addresses);
            }

            addresses.AddAddress(address);
        }

        public static void AddAddress(this StateAddress stateAddresses, Address address)
        {
            if (!stateAddresses.Addresses.Contains(address)) stateAddresses.Addresses.Add(address);
        }

        public static void AddAppointment(this Pdb pdb, string state, Appointment appointment)
        {
            var licenses = pdb.GetStateLicense(state);

            if (licenses == null)
            {
                licenses = new StateLicense { State = state };
                pdb.AddStateLicense(licenses);
            }

            licenses.AddAppointment(appointment);
        }

        public static void AddAppointment(this StateLicense licenses, Appointment appointment)
        {
            if (licenses == null) return;
            if (!licenses.Appointments.Contains(appointment)) licenses.Appointments.Add(appointment);
        }

        public static void AddContactInfo(this Pdb pdb, string state)
        {
            pdb.AddContactInfo(new ContactInfos { State = state });
        }

        public static void AddContactInfo(this Pdb pdb, string state, ContactInfo contactInfo)
        {
            pdb.AddContactInfo(new ContactInfos { State = state, ContactInfo = contactInfo });
        }

        public static void AddContactInfo(this Pdb pdb, ContactInfos contactInfos)
        {
            pdb.RemoveContactInfo(contactInfos.State);
            pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(contactInfos);
        }

        public static void AddLicense(this Pdb pdb, string state, License license)
        {
            var licenses = pdb.GetStateLicense(state);
            if (licenses == null)
            {
                licenses = new StateLicense { State = state };
                pdb.AddStateLicense(licenses);
            }

            licenses.AddLicense(license);
        }

        public static void AddLicense(this StateLicense licenses, License license)
        {
            if (!licenses.Licenses.Contains(license)) licenses.Licenses.Add(license);
        }

        public static void AddStateAddress(this Pdb pdb, string state)
        {
            pdb.AddStateAddress(new StateAddress { State = state });
        }

        public static void AddStateAddress(this Pdb pdb, StateAddress stateAddress)
        {
            pdb.RemoveStateAddress(stateAddress.State);
            pdb.Producer.Individual.EntityBiographic.StateAddresses.Add(stateAddress);
        }

        public static void AddStateLicense(this Pdb pdb, string state)
        {
            pdb.AddStateLicense(new StateLicense { State = state });
        }

        public static void AddStateLicense(this Pdb pdb, StateLicense stateLicense)
        {
            pdb.RemoveStateLicense(stateLicense.State);
            pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(stateLicense);
        }

        public static Biographic GetBiographic(this Pdb pdb)
        {
            try
            {
                return pdb.Producer.Individual.EntityBiographic.Biographic;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public static ContactInfo GetContactInfo(this Pdb pdb, string state)
        {
            var cis = pdb.GetContactInfos(state);
            return (cis != null) ? cis.ContactInfo : null;
        }

        public static ContactInfos GetContactInfos(this Pdb pdb, string state)
        {
            try
            {
                return pdb.Producer.Individual.EntityBiographic.ContactInfos.Where(_ => _.State == state).FirstOrDefault();
            }
            catch (NullReferenceException nre)
            {
                if (pdb.Producer == null)
                {
                    throw new NullReferenceException(Messages.MissingProducer);
                }
                else if (pdb.Producer.Individual == null)
                {
                    throw new NullReferenceException(Messages.MissingIndividual);
                }
                else if (pdb.Producer.Individual.EntityBiographic == null)
                {
                    throw new NullReferenceException(Messages.MissingEntityBiographic);
                }
                else
                {
                    throw nre;
                }
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public static License GetLicenseByClass(this StateLicense licenses, string licenseClass)
        {
            return (licenses == null) ? null : licenses.Licenses.Where(_ => _.LicenseClass == licenseClass).FirstOrDefault();
        }

        public static License GetLicenseByClassCode(this StateLicense licenses, string licenseClassCode)
        {
            return (licenses == null) ? null : licenses.Licenses.Where(_ => _.LicenseClassCode == licenseClassCode).FirstOrDefault();
        }

        public static StateAddress GetStateAddress(this Pdb pdb, string state)
        {
            try
            {
                return pdb.Producer.Individual.EntityBiographic.StateAddresses.Where(_ => _.State == state).FirstOrDefault();
            }
            catch (NullReferenceException nre)
            {
                if (pdb.Producer == null)
                {
                    throw new NullReferenceException(Messages.MissingProducer);
                }
                else if (pdb.Producer.Individual == null)
                {
                    throw new NullReferenceException(Messages.MissingIndividual);
                }
                else if (pdb.Producer.Individual.EntityBiographic == null)
                {
                    throw new NullReferenceException(Messages.MissingEntityBiographic);
                }
                else
                {
                    throw nre;
                }
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public static StateLicense GetStateLicense(this Pdb pdb, string state)
        {
            try
            {
                return pdb.Producer.Individual.ProducerLicensing.StateLicenses.Where(_ => _.State == state).FirstOrDefault();
            }
            catch (NullReferenceException nre)
            {
                if (pdb.Producer == null)
                {
                    throw new NullReferenceException(Messages.MissingProducer);
                }
                else if (pdb.Producer.Individual == null)
                {
                    throw new NullReferenceException(Messages.MissingIndividual);
                }
                else if (pdb.Producer.Individual.ProducerLicensing == null)
                {
                    throw new NullReferenceException(Messages.MissingProducerLicensing);
                }
                else
                {
                    throw nre;
                }
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public static void RemoveContactInfo(this Pdb pdb, string state)
        {
            pdb.Producer.Individual.EntityBiographic.ContactInfos.RemoveAll(_ => _.State == state);
        }

        public static void RemoveLicenseByClass(this StateLicense licenses, string licenseClass)
        {
            if (licenses != null) licenses.Licenses.RemoveAll(_ => _.LicenseClass == licenseClass);
        }

        public static void RemoveLicenseByClassCode(this StateLicense licenses, string licenseClassCode)
        {
            if (licenses != null) licenses.Licenses.RemoveAll(_ => _.LicenseClassCode == licenseClassCode);
        }

        public static void RemoveStateAddress(this Pdb pdb, string state)
        {
            pdb.Producer.Individual.EntityBiographic.StateAddresses.RemoveAll(_ => _.State == state);
        }

        public static void RemoveStateLicense(this Pdb pdb, string state)
        {
            pdb.Producer.Individual.ProducerLicensing.StateLicenses.RemoveAll(_ => _.State == state);
        }

        public static void SetBiographic(this Pdb pdb, Biographic biographic)
        {
            if (pdb.Producer == null) pdb.Producer = new Producer();
            pdb.Producer.Individual.EntityBiographic.Biographic = biographic;
        }

        public static class Messages
        {
            public const string AddressMissingState = "Cannot add an address without a value for the State property.";
            public const string MissingProducer = "Pdb.Produder is not set to an instance of an object";
            public const string MissingIndividual = "Pdb.Produder.Individual is not set to an instance of an object";
            public const string MissingEntityBiographic = "Pdb.Produder.Individual.EntityBiographic is not set to an instance of an object";
            public const string MissingProducerLicensing = "Pdb.Produder.Individual.ProducerLicensing is not set to an instance of an object";
        }
    }
}