using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class EntryData : IEquatable<EntryData>, IComparable<EntryData>
    {
        public string firstname;
        public string lastname = "";

        public EntryData (string firstname)
        {
            this.firstname = firstname;
        }

        public EntryData (string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }

        public int CompareTo(EntryData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            if (Firstname.CompareTo(other.Firstname) == 0)
            {
                return Lastname.CompareTo(other.Lastname);
            }
            return Firstname.CompareTo(other.Firstname);
        }

        public bool Equals(EntryData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (Firstname == other.Firstname)
            {
                if (Lastname == other.Lastname)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (firstname + lastname).GetHashCode();
        }

        public override string ToString()
        {
            return firstname;
        }

    }
}
