using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Shouldly;
using Nipr.Parser.Nipr;

namespace Nipr.Parser.Test.Nipr
{
    public class PdbExtensionTests
    {
        public class AddAddress
        {
            private const string _state1 = "AZ";
            private const string _state2 = "UT";
            private const string _city1 = "Tempe";
            private const string _city2 = "Sandy";
            private readonly Pdb _pdb;
            private readonly Address _address1;
            private readonly Address _address2;
            private readonly StateAddress _addresses;

            public AddAddress()
            {
                _pdb = Pdb.GenerateEmpty();
                _address1 = new Address { State = _state1, City = _city1 };
                _address2 = new Address { State = _state2, City = _city2 };

                _addresses = new StateAddress();

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(0);
                _addresses.Addresses.Count.ShouldBe(0);
            }

            [Fact]
            public void throws_exception_when_state_is_null_or_empty()
            {
                var pdb = new Pdb();

                var exa = Should.Throw<ArgumentException>(() =>
                {
                    pdb.AddAddress(new Address());
                });

                var exb = Should.Throw<ArgumentException>(() =>
                {
                    pdb.AddAddress(new Address { State = null });
                });

                var exc = Should.Throw<ArgumentException>(() =>
                {
                    pdb.AddAddress(null, new Address());
                });

                var exd = Should.Throw<ArgumentException>(() =>
                {
                    pdb.AddAddress(string.Empty, new Address());
                });

                exa.Message.ShouldBe(PdbExtensions.Messages.AddressMissingState);
                exb.Message.ShouldBe(PdbExtensions.Messages.AddressMissingState);
                exc.Message.ShouldBe(PdbExtensions.Messages.AddressMissingState);
                exd.Message.ShouldBe(PdbExtensions.Messages.AddressMissingState);
            }

            [Fact]
            public void adds_address_to_pdb()
            {
                _pdb.AddAddress(_address1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].ShouldBe(_address1);
            }

            [Fact]
            public void updates_missing_state_before_adding_address_to_pdb()
            {
                _address1.State = null;
                _pdb.AddAddress(_state1, _address1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].ShouldBe(_address1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].State.ShouldBe(_state1);
            }

            [Fact]
            public void does_not_alter_existing_state_before_adding_address_to_pdb()
            {
                _pdb.AddAddress(_state1, _address2);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].ShouldBe(_address2);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].State.ShouldBe(_state2);
            }

            [Fact]
            public void adds_address_to_pdb_with_existing_state_addresses()
            {
                _pdb.AddAddress(_state1, _address1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].ShouldBe(_address1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].City.ShouldBe(_city1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].State.ShouldBe(_state1);

                _pdb.AddAddress(_state1, _address2);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses.Count.ShouldBe(2);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].ShouldBe(_address1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].City.ShouldBe(_city1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[1].ShouldBe(_address2);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[1].City.ShouldBe(_city2);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[1].State.ShouldBe(_state2);
            }

            [Fact]
            public void does_not_add_duplicate_addresses_to_pdb()
            {
                _pdb.AddAddress(_state1, _address1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].ShouldBe(_address1);

                _pdb.AddAddress(_state1, _address1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].Addresses[0].ShouldBe(_address1);
            }

            [Fact]
            public void adds_address_to_state_address()
            {
                _addresses.AddAddress(_address1);

                _addresses.Addresses.Count.ShouldBe(1);
                _addresses.Addresses[0].ShouldBe(_address1);
            }

            [Fact]
            public void does_not_add_duplicate_addresses_to_stateaddress()
            {
                _addresses.AddAddress(_address1);

                _addresses.Addresses.Count.ShouldBe(1);
                _addresses.Addresses[0].ShouldBe(_address1);

                _addresses.AddAddress(_address1);

                _addresses.Addresses.Count.ShouldBe(1);
                _addresses.Addresses[0].ShouldBe(_address1);
            }
        }

        public class AddAppointment
        {
            private const string _state = "AZ";
            private readonly Pdb _pdb;
            private readonly Appointment _appointment;
            private readonly StateLicense _licenses;

            public AddAppointment()
            {
                _pdb = Pdb.GenerateEmpty();
                _appointment = new Appointment();
                _licenses = new StateLicense();

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(0);
                _licenses.Appointments.Count.ShouldBe(0);
            }

            [Fact]
            public void add_to_pdb_generates_statelicense_if_missing()
            {
                _pdb.AddAppointment(_state, _appointment);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments[0].ShouldBe(_appointment);
            }

            [Fact]
            public void adds_to_pdb_with_existing_statelicense()
            {
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(new StateLicense { State = _state });
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments.Count.ShouldBe(0);

                _pdb.AddAppointment(_state, _appointment);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments[0].ShouldBe(_appointment);
            }

            [Fact]
            public void does_not_add_duplicate_to_pdb()
            {
                _pdb.AddAppointment(_state, _appointment);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments[0].ShouldBe(_appointment);

                _pdb.AddAppointment(_state, _appointment);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Appointments[0].ShouldBe(_appointment);
            }

            [Fact]
            public void does_not_add_to_null_statelicence()
            {
                StateLicense license = null;
                Should.NotThrow(() =>
                {
                    license.AddAppointment(_appointment);
                });
                license.ShouldBeNull();
            }

            [Fact]
            public void adds_to_statelicense()
            {
                _licenses.AddAppointment(_appointment);
                _licenses.Appointments.Count.ShouldBe(1);
                _licenses.Appointments[0].ShouldBe(_appointment);

                _licenses.AddAppointment(new Appointment());
                _licenses.Appointments.Count.ShouldBe(2);
                _licenses.Appointments[0].ShouldBe(_appointment);
                _licenses.Appointments[1].ShouldNotBe(_appointment);
            }

            [Fact]
            public void does_not_add_duplicate_to_statelicense()
            {
                _licenses.AddAppointment(_appointment);

                _licenses.Appointments.Count.ShouldBe(1);
                _licenses.Appointments[0].ShouldBe(_appointment);

                _licenses.AddAppointment(_appointment);

                _licenses.Appointments.Count.ShouldBe(1);
                _licenses.Appointments[0].ShouldBe(_appointment);
            }
        }

        public class AddContactInfo
        {
            private const string _state = "AZ";
            private readonly Pdb _pdb;
            private readonly ContactInfo _contactInfo;
            private readonly ContactInfos _contactInfos;
            private readonly ContactInfos _replacement;

            public AddContactInfo()
            {
                _pdb = Pdb.GenerateEmpty();
                _contactInfo = new ContactInfo();
                _contactInfos = new ContactInfos { State = _state, ContactInfo = _contactInfo };
                _replacement = new ContactInfos { State = _state, ContactInfo = new ContactInfo() };

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(0);
            }

            [Fact]
            public void adds_empty_contactinfos_for_state()
            {
                _pdb.AddContactInfo(_state);

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].State.ShouldBe(_state);
            }

            [Fact]
            public void adds_contactinfo_to_contactinfos()
            {
                _pdb.AddContactInfo(_state, _contactInfo);

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].ContactInfo.ShouldBe(_contactInfo);
            }

            [Fact]
            public void adds_contactinfos()
            {
                _pdb.AddContactInfo(_contactInfos);

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].ShouldBe(_contactInfos);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].ContactInfo.ShouldBe(_contactInfo);
            }

            [Fact]
            public void replaces_contactinfo_in_contactinfos()
            {
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(_contactInfos);

                _pdb.AddContactInfo(_replacement);

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].ShouldBe(_replacement);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].ContactInfo.ShouldNotBe(_contactInfo);
            }
        }

        public class AddLicense
        {
            private const string _state = "AZ";
            private readonly Pdb _pdb;
            private readonly StateLicense _licenses;
            private readonly License _license;

            public AddLicense()
            {
                _pdb = Pdb.GenerateEmpty();
                _licenses = new StateLicense { State = _state };
                _license = new License();

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(0);
                _licenses.Licenses.Count.ShouldBe(0);
            }

            [Fact]
            public void add_to_pdb_without_existing_licenses()
            {
                _pdb.AddLicense(_state, _license);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses[0].ShouldBe(_license);
            }

            [Fact]
            public void add_to_pdb_with_existing_licenses()
            {
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(_licenses);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(new StateLicense { State = "UT" });

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(2);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].ShouldBe(_licenses);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses.Count.ShouldBe(0);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[1].Licenses.Count.ShouldBe(0);

                _pdb.AddLicense(_state, _license);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(2);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].ShouldBe(_licenses);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[1].Licenses.Count.ShouldBe(0);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses[0].ShouldBe(_license);
            }

            [Fact]
            public void does_not_add_duplicate_to_pdb()
            {
                _pdb.AddLicense(_state, _license);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses[0].ShouldBe(_license);

                _pdb.AddLicense(_state, _license);
                // _pdb.AddLicense(_state, new License { LicenseClass = _license.LicenseClass, LicenseClassCode = _license.LicenseClassCode });

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].Licenses[0].ShouldBe(_license);
            }

            [Fact]
            public void add_to_statelicense()
            {
                _licenses.AddLicense(_license);

                _licenses.Licenses.Count.ShouldBe(1);
                _licenses.Licenses[0].ShouldBe(_license);
            }

            [Fact]
            public void does_not_add_duplicate_to_statelicense()
            {
                _licenses.AddLicense(_license);

                _licenses.Licenses.Count.ShouldBe(1);
                _licenses.Licenses[0].ShouldBe(_license);

                _licenses.AddLicense(_license);
                // _licenses.AddLicense(new License { LicenseClass = _license.LicenseClass, LicenseClassCode = _license.LicenseClassCode });

                _licenses.Licenses.Count.ShouldBe(1);
                _licenses.Licenses[0].ShouldBe(_license);
            }
        }

        public class AddStateAddress
        {
            private const string _state = "AZ";
            private readonly Pdb _pdb;
            private readonly StateAddress _stateAddress1;
            private readonly StateAddress _stateAddress2;

            public AddStateAddress()
            {
                _pdb = Pdb.GenerateEmpty();
                _stateAddress1 = new StateAddress { State = _state };
                _stateAddress2 = new StateAddress { State = _state };

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(0);
            }

            [Fact]
            public void adds_empty_stateaddress_for_state()
            {
                _pdb.AddStateAddress(_state);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state);
            }

            [Fact]
            public void adds_stateaddres()
            {
                _pdb.AddStateAddress(_stateAddress1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].ShouldBe(_stateAddress1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state);
            }

            [Fact]
            public void replaces_state_address()
            {
                _pdb.AddStateAddress(_stateAddress1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].ShouldBe(_stateAddress1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state);

                _pdb.AddStateAddress(_stateAddress2);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].ShouldBe(_stateAddress2);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state);
            }
        }

        public class AddStateLicense
        {
            private const string _state = "AZ";
            private readonly Pdb _pdb;
            private readonly StateLicense _stateLicense1;
            private readonly StateLicense _stateLicense2;

            public AddStateLicense()
            {
                _pdb = Pdb.GenerateEmpty();
                _stateLicense1 = new StateLicense { State = _state };
                _stateLicense2 = new StateLicense { State = _state };

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(0);
            }

            [Fact]
            public void adds_empty_stateaddress_for_state()
            {
                _pdb.AddStateLicense(_state);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
            }

            [Fact]
            public void adds_stateaddres()
            {
                _pdb.AddStateLicense(_stateLicense1);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].ShouldBe(_stateLicense1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
            }

            [Fact]
            public void replaces_state_address()
            {
                _pdb.AddStateLicense(_stateLicense1);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].ShouldBe(_stateLicense1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);

                _pdb.AddStateLicense(_stateLicense2);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].ShouldBe(_stateLicense2);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state);
            }
        }

        public class BiographicGetterAndSetter
        {
            private readonly Pdb _pdb;
            private readonly Biographic _biographic1;
            private readonly Biographic _biographic2;

            public BiographicGetterAndSetter()
            {
                _biographic1 = new Biographic { FirstName = "John", LastName = "Doe" };
                _biographic2 = new Biographic { FirstName = "Jane", LastName = "Doe" };
                _pdb = Pdb.Generate(_biographic1);
            }

            [Fact]
            public void getbiographic_returns_null_when_parent_elements_are_missing()
            {
                _pdb.Producer.Individual.EntityBiographic.Biographic = null;
                _pdb.GetBiographic().ShouldBeNull();

                _pdb.Producer.Individual.EntityBiographic = null;
                _pdb.GetBiographic().ShouldBeNull();

                _pdb.Producer.Individual = null;
                _pdb.GetBiographic().ShouldBeNull();

                _pdb.Producer = null;
                _pdb.GetBiographic().ShouldBeNull();
            }

            [Fact]
            public void getbiographic_returns_biographic()
            {
                _pdb.GetBiographic().ShouldBe(_biographic1);
            }

            [Fact]
            public void setbiographic_adds_missing_parent_elements()
            {
                var pdb = new Pdb();
                pdb.Producer.ShouldBeNull();

                pdb.SetBiographic(_biographic1);

                pdb.Producer.Individual.EntityBiographic.Biographic.ShouldBe(_biographic1);
            }

            [Fact]
            public void setbiographic_overrides_existing_biographic()
            {
                _pdb.Producer.Individual.EntityBiographic.Biographic.ShouldBe(_biographic1);

                _pdb.SetBiographic(_biographic2);

                _pdb.Producer.Individual.EntityBiographic.Biographic.ShouldBe(_biographic2);
            }
        }

        public class GetContactInfo
        {
            private const string _email1 = "test1@email.com";
            private const string _email2 = "test2@email.com";
            private const string _state1 = "AZ";
            private const string _state2 = "UT";
            private readonly Pdb _pdb;
            private readonly ContactInfo _contactInfo1;
            private readonly ContactInfo _contactInfo2;

            public GetContactInfo()
            {
                _pdb = Pdb.GenerateEmpty();
                _contactInfo1 = new ContactInfo { Email = _email1 };
                _contactInfo2 = new ContactInfo { Email = _email2 };
            }

            [Fact]
            public void throws_exception_when_parent_elements_are_missing()
            {
                _pdb.Producer.Individual.EntityBiographic = null;
                var exa = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetContactInfos(_state1);
                });

                _pdb.Producer.Individual = null;
                var exb = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetContactInfos(_state1);
                });

                _pdb.Producer = null;
                var exc = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetContactInfos(_state1);
                });

                exa.Message.ShouldBe(PdbExtensions.Messages.MissingEntityBiographic);
                exb.Message.ShouldBe(PdbExtensions.Messages.MissingIndividual);
                exc.Message.ShouldBe(PdbExtensions.Messages.MissingProducer);
            }

            [Fact]
            public void returns_null_when_contactinfos_does_not_exist()
            {
                _pdb.Producer.Individual.EntityBiographic.ContactInfos = null;
                _pdb.GetContactInfos(_state1).ShouldBeNull();
            }

            [Fact]
            public void returns_null_when_contactinfo_for_state_does_not_exist()
            {
                _pdb.GetContactInfo(_state1).ShouldBeNull();
            }

            [Fact]
            public void returns_contactinfo_for_existing_state_contactinfos()
            {
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(new ContactInfos { State = _state1, ContactInfo = _contactInfo1 });
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(new ContactInfos { State = _state2, ContactInfo = _contactInfo2 });

                _pdb.GetContactInfo(_state1).ShouldBe(_contactInfo1);
                _pdb.GetContactInfo(_state2).ShouldBe(_contactInfo2);
            }
        }

        public class GetLicense
        {
            private const string _className1 = "Class Name 1";
            private const string _className2 = "Class Name 2";
            private const string _classCode1 = "123";
            private const string _classCode2 = "456";
            private readonly License _license1;
            private readonly License _license2;
            private readonly StateLicense _licenses;

            public GetLicense()
            {
                _license1 = new License { LicenseClass = _className1, LicenseClassCode = _classCode1 };
                _license2 = new License { LicenseClass = _className2, LicenseClassCode = _classCode2 };
                _licenses = new StateLicense { Licenses = new List<License> { _license1, _license2 } };
            }

            [Fact]
            public void getlicensebyclass_returns_null_when_licenses_do_not_exist()
            {
                StateLicense licenses = null;
                licenses.GetLicenseByClass(_className1).ShouldBeNull();

                licenses = new StateLicense();
                licenses.GetLicenseByClass(_className1).ShouldBeNull();

                licenses.Licenses.Add(_license2);
                licenses.GetLicenseByClass(_className1).ShouldBeNull();
            }

            [Fact]
            public void getlicensebyclass_returns_requested_license()
            {
                var license1 = _licenses.GetLicenseByClass(_className1);
                license1.ShouldBe(_license1);
                license1.LicenseClass.ShouldBe(_className1);

                var license2 = _licenses.GetLicenseByClass(_className2);
                license2.ShouldBe(_license2);
                license2.LicenseClass.ShouldBe(_className2);
            }

            [Fact]
            public void getlicensebyclasscode_returns_null_when_licenses_do_not_exist()
            {
                StateLicense licenses = null;
                licenses.GetLicenseByClassCode(_classCode1).ShouldBeNull();

                licenses = new StateLicense();
                licenses.GetLicenseByClassCode(_classCode1).ShouldBeNull();

                licenses.Licenses.Add(_license2);
                licenses.GetLicenseByClassCode(_classCode1).ShouldBeNull();
            }

            [Fact]
            public void getlicensebyclasscode_returns_requested_license()
            {
                var license1 = _licenses.GetLicenseByClassCode(_classCode1);
                license1.ShouldBe(_license1);
                license1.LicenseClassCode.ShouldBe(_classCode1);

                var license2 = _licenses.GetLicenseByClassCode(_classCode2);
                license2.ShouldBe(_license2);
                license2.LicenseClassCode.ShouldBe(_classCode2);
            }
        }

        public class GetStateAddress
        {
            private const string _state1 = "AZ";
            private const string _state2 = "UT";
            private readonly Pdb _pdb;
            private readonly StateAddress _stateAddresses;

            public GetStateAddress()
            {
                _stateAddresses = new StateAddress { State = _state1 };
                _pdb = Pdb.GenerateEmpty();
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Add(_stateAddresses);
            }

            [Fact]
            public void throws_exception_when_parent_elements_are_missing()
            {
                _pdb.Producer.Individual.EntityBiographic = null;
                var exa = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetStateAddress(_state1);
                });

                _pdb.Producer.Individual = null;
                var exb = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetStateAddress(_state1);
                });

                _pdb.Producer = null;
                var exc = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetStateAddress(_state1);
                });

                exa.Message.ShouldBe(PdbExtensions.Messages.MissingEntityBiographic);
                exb.Message.ShouldBe(PdbExtensions.Messages.MissingIndividual);
                exc.Message.ShouldBe(PdbExtensions.Messages.MissingProducer);
            }

            [Fact]
            public void returns_null_when_stateaddresses_is_missing()
            {
                _pdb.Producer.Individual.EntityBiographic.StateAddresses = null;
                _pdb.GetStateAddress(_state1).ShouldBeNull();
            }

            [Fact]
            public void returns_null_when_no_matching_stateaddresses()
            {
                _pdb.GetStateAddress(_state2).ShouldBeNull();
            }

            [Fact]
            public void returns_addresses_for_state()
            {
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Add(new StateAddress { State = _state2 });
                _pdb.GetStateAddress(_state1).ShouldBe(_stateAddresses);
            }
        }

        public class GetStateLicense
        {
            private const string _licenseNumber1 = "123";
            private const string _licenseNumber2 = "456";
            private const string _state1 = "AZ";
            private const string _state2 = "UT";
            private readonly Pdb _pdb;
            private readonly StateLicense _licenses1;
            private readonly StateLicense _licenses2;
            private readonly License _license1;
            private readonly License _license2;

            public GetStateLicense()
            {
                _license1 = new License { LicenseNumber = _licenseNumber1 };
                _license2 = new License { LicenseNumber = _licenseNumber2 };

                _licenses1 = new StateLicense { State = _state1, Licenses = new List<License> { _license1 } };
                _licenses2 = new StateLicense { State = _state2, Licenses = new List<License> { _license2 } };

                _pdb = Pdb.GenerateEmpty();
            }

            [Fact]
            public void throws_exception_when_parent_elements_are_missing()
            {
                _pdb.Producer.Individual.ProducerLicensing = null;
                var exa = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetStateLicense(_state1);
                });

                _pdb.Producer.Individual = null;
                var exb = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetStateLicense(_state1);
                });

                _pdb.Producer = null;
                var exc = Should.Throw<NullReferenceException>(() =>
                {
                    _pdb.GetStateLicense(_state1);
                });

                exa.Message.ShouldBe(PdbExtensions.Messages.MissingProducerLicensing);
                exb.Message.ShouldBe(PdbExtensions.Messages.MissingIndividual);
                exc.Message.ShouldBe(PdbExtensions.Messages.MissingProducer);
            }

            [Fact]
            public void returns_null_when_statelicenses_is_missing()
            {
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses = null;
                _pdb.GetStateLicense(_state1).ShouldBeNull();
            }

            [Fact]
            public void returns_null_when_no_matching_statelicenses()
            {
                _pdb.GetStateLicense(_state1).ShouldBeNull();
            }

            [Fact]
            public void returns_licenses_for_state()
            {
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(_licenses1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(_licenses2);

                var licenses = _pdb.GetStateLicense(_state1);

                licenses.ShouldBe(_licenses1);
            }
        }

        public class RemoveContactInfo
        {
            private const string _state1 = "AZ";
            private const string _state2 = "UT";
            private readonly Pdb _pdb;

            public RemoveContactInfo()
            {
                _pdb = Pdb.GenerateEmpty();
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(new ContactInfos { State = _state2 });

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
            }

            [Fact]
            public void removes_single_for_state()
            {
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(new ContactInfos { State = _state1 });
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(2);

                _pdb.RemoveContactInfo(_state1);

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].State.ShouldBe(_state2);
            }

            [Fact]
            public void removes_multiple_for_state()
            {
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(new ContactInfos { State = _state1 });
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Add(new ContactInfos { State = _state2 });
                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(3);

                _pdb.RemoveContactInfo(_state2);

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].State.ShouldBe(_state1);
            }

            [Fact]
            public void removes_nothing_when_no_matching_states()
            {
                _pdb.RemoveContactInfo(_state1);

                _pdb.Producer.Individual.EntityBiographic.ContactInfos.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.ContactInfos[0].State.ShouldBe(_state2);
            }
        }

        public class RemoveLicense
        {
            private const string _licenseClass1 = "License Class 1";
            private const string _licenseClass2 = "License Class 2";
            private const string _licenseCode1 = "123";
            private const string _licenseCode2 = "456";
            private readonly StateLicense _licenses;

            public RemoveLicense()
            {
                _licenses = new StateLicense
                {
                    Licenses = new List<License>
                    {
                        new License { LicenseClass = _licenseClass1, LicenseClassCode = _licenseCode1 },
                        new License { LicenseClass = _licenseClass2, LicenseClassCode = _licenseCode2 }
                    }
                };

                _licenses.Licenses.Count.ShouldBe(2);
            }

            [Fact]
            public void by_class_removes_by_class()
            {
                _licenses.RemoveLicenseByClass(_licenseClass1);

                _licenses.Licenses.Count.ShouldBe(1);
                _licenses.Licenses[0].LicenseClass.ShouldBe(_licenseClass2);
                _licenses.Licenses[0].LicenseClassCode.ShouldBe(_licenseCode2);
            }

            [Fact]
            public void by_class_does_not_remove_when_no_match_found()
            {
                _licenses.RemoveLicenseByClass("No Match");

                _licenses.Licenses.Count.ShouldBe(2);
                _licenses.Licenses[0].LicenseClass.ShouldBe(_licenseClass1);
                _licenses.Licenses[0].LicenseClassCode.ShouldBe(_licenseCode1);
                _licenses.Licenses[1].LicenseClass.ShouldBe(_licenseClass2);
                _licenses.Licenses[1].LicenseClassCode.ShouldBe(_licenseCode2);
            }

            [Fact]
            public void by_code_removes_by_code()
            {
                _licenses.RemoveLicenseByClassCode(_licenseCode1);

                _licenses.Licenses.Count.ShouldBe(1);
                _licenses.Licenses[0].LicenseClass.ShouldBe(_licenseClass2);
                _licenses.Licenses[0].LicenseClassCode.ShouldBe(_licenseCode2);
            }

            [Fact]
            public void by_code_does_not_remove_when_no_match_found()
            {
                _licenses.RemoveLicenseByClassCode("No Match");

                _licenses.Licenses.Count.ShouldBe(2);
                _licenses.Licenses[0].LicenseClass.ShouldBe(_licenseClass1);
                _licenses.Licenses[0].LicenseClassCode.ShouldBe(_licenseCode1);
                _licenses.Licenses[1].LicenseClass.ShouldBe(_licenseClass2);
                _licenses.Licenses[1].LicenseClassCode.ShouldBe(_licenseCode2);
            }
        }

        public class RemoveStateAddress
        {
            private const string _state1 = "AZ";
            private const string _state2 = "UT";
            private readonly Pdb _pdb;

            public RemoveStateAddress()
            {
                _pdb = Pdb.GenerateEmpty();
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Add(new StateAddress { State = _state2 });

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
            }

            [Fact]
            public void removes_single_for_state()
            {
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Add(new StateAddress { State = _state1 });
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(2);

                _pdb.RemoveStateAddress(_state1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state2);
            }

            [Fact]
            public void removes_multiple_for_state()
            {
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Add(new StateAddress { State = _state1 });
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Add(new StateAddress { State = _state2 });
                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(3);

                _pdb.RemoveStateAddress(_state2);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state1);

            }

            [Fact]
            public void removes_nothing_when_no_matching_states()
            {
                _pdb.RemoveStateAddress(_state1);

                _pdb.Producer.Individual.EntityBiographic.StateAddresses.Count.ShouldBe(1);
                _pdb.Producer.Individual.EntityBiographic.StateAddresses[0].State.ShouldBe(_state2);
            }
        }

        public class RemoveStateLicense
        {
            private const string _state1 = "AZ";
            private const string _state2 = "UT";
            private readonly Pdb _pdb;

            public RemoveStateLicense()
            {
                _pdb = Pdb.GenerateEmpty();
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(new StateLicense { State = _state2 });

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
            }

            [Fact]
            public void removes_single_for_state()
            {
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(new StateLicense { State = _state1 });
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(2);

                _pdb.RemoveStateLicense(_state1);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state2);
            }

            [Fact]
            public void removes_multiple_for_state()
            {
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(new StateLicense { State = _state1 });
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Add(new StateLicense { State = _state2 });
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(3);

                _pdb.RemoveStateLicense(_state2);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state1);
            }

            [Fact]
            public void removes_nothing_when_no_matching_states()
            {
                _pdb.RemoveStateLicense(_state1);

                _pdb.Producer.Individual.ProducerLicensing.StateLicenses.Count.ShouldBe(1);
                _pdb.Producer.Individual.ProducerLicensing.StateLicenses[0].State.ShouldBe(_state2);
            }
        }
    }
}